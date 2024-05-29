using MySql.Data.MySqlClient;
using Mysqlx.Resultset;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Resources;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PlayerMgtForm
{
    public partial class MainForm : Form
    {
        DataSet ds;
        private static String connStr = "Server=localhost;Database=playermgtdb;Uid=root;Pwd=1234";
        MySqlConnection conn = null;
        private Point mousePoint;
        private String imagePath = Path.GetFullPath("resources\\").Replace("bin\\Debug\\", "");
        public MainForm()
        {
            InitializeComponent();
            lbUser.Text = LoginForm.Id;
            btnLogout.Click += BtnLogout_Click;
            btnHome.Click += BtnHome_Click;
            btnPlayer.Click += BtnPlayer_Click;
            btnStaff.Click += BtnStaff_Click;
            btnFomation.Click += BtnFomation_Click;
            btnInfo.Click += BtnInfo_Click;
            lbBack.Click += LbBack_Click;
            conn = new MySqlConnection(connStr);
            #region 메인화면
            try
            {
                //구단 순위               
                conn.Open();
                ds = new DataSet();
                String selectQuery = "select dense_rank() over(order by cPoints desc) as '순위', cName as '구단', cPlayed as '경기', cPoints as '승점', cWon as '승', cDraw as '무', cLoss as '패' from clubs";
                MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, conn);
                adapter.Fill(ds, "clubs");
                teamRankDGV.DataSource = ds.Tables["clubs"];
                teamRankDGV.Columns["구단"].FillWeight = 160;

                //최다 득점
                selectQuery = "SELECT rPlayed ,pName, pNum, pImage, pPosition, rGoal, rAssist FROM playerrecord, player where playerrecord.rId = player.pId and rGoal = (select max(rGoal) from playerrecord) order by rPlayed desc";
                adapter = new MySqlDataAdapter(selectQuery, conn);
                adapter.Fill(ds, "playerrecord1");
                Image image = Image.FromFile(imagePath + "Player\\" + ds.Tables["playerrecord1"].Rows[0]["pImage"].ToString());
                picGoalMvp.Image = image;
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                sb1.Append(ds.Tables["playerrecord1"].Rows[0]["pPosition"].ToString());
                sb1.Append(" " + ds.Tables["playerrecord1"].Rows[0]["pNum"].ToString());
                sb1.Append(" " + ds.Tables["playerrecord1"].Rows[0]["pName"].ToString());
                lbGoalMvp.Text = sb1.ToString();
                sb2.Append(ds.Tables["playerrecord1"].Rows[0]["rGoal"].ToString() + " 골 ");
                sb2.Append(ds.Tables["playerrecord1"].Rows[0]["rAssist"].ToString() + " 도움 ");
                sb2.Append("(" + ds.Tables["playerrecord1"].Rows[0]["rPlayed"].ToString() + "경기 출전)");
                lbGoalRecord.Text = sb2.ToString();
                sb1.Clear();
                sb2.Clear();

                //최다 도움
                selectQuery = "SELECT rPlayed ,pName, pNum, pImage, pPosition, rGoal, rAssist FROM playerrecord, player where playerrecord.rId = player.pId and rAssist = (select max(rAssist) from playerrecord) order by rPlayed desc";
                adapter = new MySqlDataAdapter(selectQuery, conn);
                adapter.Fill(ds, "playerrecord2");
                image = Image.FromFile(imagePath + "Player\\" + ds.Tables["playerrecord2"].Rows[0]["pImage"].ToString());
                picAssistMvp.Image = image;
                sb1.Append(ds.Tables["playerrecord2"].Rows[0]["pPosition"].ToString());
                sb1.Append(" " + ds.Tables["playerrecord2"].Rows[0]["pNum"].ToString());
                sb1.Append(" " + ds.Tables["playerrecord2"].Rows[0]["pName"].ToString());
                lbAssistMvp.Text = sb1.ToString();
                sb2.Append(ds.Tables["playerrecord2"].Rows[0]["rGoal"].ToString() + " 골 ");
                sb2.Append(ds.Tables["playerrecord2"].Rows[0]["rAssist"].ToString() + " 도움 ");
                sb2.Append("(" + ds.Tables["playerrecord2"].Rows[0]["rPlayed"].ToString() + "경기 출전)");
                lbAssistRecord.Text = sb2.ToString();
                sb1.Clear();
                sb2.Clear();

                //다음 경기
                selectQuery = "select gSeq, gDate, gStadium, gContent, clubs.cName, gResult, cImage, clubs.cStadium from schedule, clubs where clubs.cId = schedule.gOppTeam and gDate >= now() order by gDate desc";
                adapter = new MySqlDataAdapter(selectQuery, conn);
                adapter.Fill(ds, "schedule");

                if (ds.Tables["schedule"].Rows.Count == 0)
                {
                    lbOppTeam.Text = "리그일정이 종료되었습니다";
                }
                else
                {
                    lbDate.Text = ds.Tables["schedule"].Rows[0]["gDate"].ToString();
                    lbOppTeam.Text = "FC안양 VS " + ds.Tables["schedule"].Rows[0]["cName"].ToString();
                    lbStadium.Text = ds.Tables["schedule"].Rows[0]["cStadium"].ToString();
                    image = Image.FromFile(imagePath + "Clubs\\" + ds.Tables["schedule"].Rows[0]["cImage"].ToString());
                    picTeam1.Image = Properties.Resources.ANYANG_FC;
                    picTeam2.Image = image;
                }

                // 헤더부분
                foreach (DataRow i in ds.Tables["clubs"].Rows)
                {
                    if (i["구단"].ToString().Equals("FC안양"))
                    {
                        lbTopState.Text = "K리그2에서 " + i["순위"] + "위";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            #endregion 메인화면
            #region 선수단화면
            try
            {
                conn.Open();
                ds = new DataSet();
                //선수단 Select
                SelectPlayers();
                // 선수 상세정보
                playersDGV.CellClick += PlayersDGV_CellClick;
                //검색바
                txtSearch.KeyPress += TxtSearch_KeyPress;
                //선수추가
                btnInsert.Click += BtnInsert_Click;
                //선수수정
                btnUpdate.Click += BtnUpdate_Click;
                //선수삭제
                btnDelete.Click += BtnDelete_Click;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            #endregion
            #region 포메이션화면
            try
            {
                conn.Open();
                SelectFomation();
                picGK.Click += PicGK_Click;
                picGK.MouseDown += PicGK_MouseDown;
                picGK.MouseMove += PicGK_MouseMove;
                picLB.Click += PicLB_Click;
                picLB.MouseDown += PicLB_MouseDown;
                picLB.MouseMove += PicLB_MouseMove;
                picCB1.Click += PicCB1_Click;
                picCB1.MouseDown += PicCB1_MouseDown;
                picCB1.MouseMove += PicCB1_MouseMove;
                picCB2.Click += PicCB2_Click;
                picCB2.MouseDown += PicCB2_MouseDown;
                picCB2.MouseMove += PicCB2_MouseMove;
                picRB.Click += PicRB_Click;
                picRB.MouseDown += PicRB_MouseDown;
                picRB.MouseMove += PicRB_MouseMove;
                picLDM.Click += PicLDM_Click;
                picLDM.MouseDown += PicLDM_MouseDown;
                picLDM.MouseMove += PicLDM_MouseMove;
                picRDM.Click += PicRDM_Click;
                picRDM.MouseDown += PicRDM_MouseDown;
                picRDM.MouseMove += PicRDM_MouseMove;
                picLW.Click += PicLW_Click;
                picLW.MouseDown += PicLW_MouseDown;
                picLW.MouseMove += PicLW_MouseMove;
                picCAM.Click += PicCAM_Click;
                picCAM.MouseDown += PicCAM_MouseDown;
                picCAM.MouseMove += PicCAM_MouseMove;
                picRW.Click += PicRW_Click;
                picRW.MouseDown += PicRW_MouseDown;
                picRW.MouseMove += PicRW_MouseMove;
                picST.Click += PicST_Click;
                picST.MouseDown += PicST_MouseDown;
                picST.MouseMove += PicST_MouseMove;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            #endregion
        }
        
        #region 위치 저장
        private void PicST_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = e.Location;
        }

        private void PicRW_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = e.Location;
        }

        private void PicCAM_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = e.Location;
        }

        private void PicLW_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = e.Location;
        }

        private void PicCB2_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = e.Location;
        }

        private void PicCB1_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = e.Location;
        }

        private void PicRDM_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = e.Location;
        }

        private void PicLDM_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = e.Location;
        }

        private void PicRB_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = e.Location;
        }

        private void PicLB_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = e.Location;
        }

        private void PicGK_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = e.Location;
        }
        #endregion
        #region 위치이동
        private void PicST_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                picST.Location = new Point(picST.Location.X + (e.X - mousePoint.X), picST.Location.Y + (e.Y - mousePoint.Y));
            }
        }

        private void PicCAM_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                picCAM.Location = new Point(picCAM.Location.X + (e.X - mousePoint.X), picCAM.Location.Y + (e.Y - mousePoint.Y));
            }
        }

        private void PicLW_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                picLW.Location = new Point(picLW.Location.X + (e.X - mousePoint.X), picLW.Location.Y + (e.Y - mousePoint.Y));
            }
        }

        private void PicRDM_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                picRDM.Location = new Point(picRDM.Location.X + (e.X - mousePoint.X), picRDM.Location.Y + (e.Y - mousePoint.Y));
            }
        }

        private void PicLDM_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                picLDM.Location = new Point(picLDM.Location.X + (e.X - mousePoint.X), picLDM.Location.Y + (e.Y - mousePoint.Y));
            }
        }

        private void PicCB2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                picCB2.Location = new Point(picCB2.Location.X + (e.X - mousePoint.X), picCB2.Location.Y + (e.Y - mousePoint.Y));
            }
        }

        private void PicCB1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                picCB1.Location = new Point(picCB1.Location.X + (e.X - mousePoint.X), picCB1.Location.Y + (e.Y - mousePoint.Y));
            }
        }

        private void PicGK_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                picGK.Location = new Point(picGK.Location.X + (e.X - mousePoint.X), picGK.Location.Y + (e.Y - mousePoint.Y));
            }
        }

        private void PicRB_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                picRB.Location = new Point(picRB.Location.X + (e.X - mousePoint.X), picRB.Location.Y + (e.Y - mousePoint.Y));
            }
        }

        private void PicLB_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                picLB.Location = new Point(picLB.Location.X + (e.X - mousePoint.X), picLB.Location.Y + (e.Y - mousePoint.Y));
            }
        }

        private void PicRW_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                picRW.Location = new Point(picRW.Location.X + (e.X - mousePoint.X), picRW.Location.Y + (e.Y - mousePoint.Y));
            }
        }
        #endregion
        #region 이미지 넣기
        private void PicST_Click(object sender, EventArgs e)
        {
            if (fomationDGV.SelectedRows.Count == 0)
            {
                return;
            }
            else
            {
                picST.Image = GetImage(fomationDGV.SelectedRows[0].Index);
                fomationDGV.Rows[fomationDGV.SelectedRows[0].Index].ReadOnly = true;
                fomationDGV.SelectedRows[0].Selected = false;
            }
        }

        private void PicRW_Click(object sender, EventArgs e)
        {
            if (fomationDGV.SelectedRows.Count == 0)
            {
                return;
            }
            else
            {
                picRW.Image = GetImage(fomationDGV.SelectedRows[0].Index);
                fomationDGV.Rows[fomationDGV.SelectedRows[0].Index].ReadOnly = true;
                fomationDGV.SelectedRows[0].Selected = false;
            }
        }

        private void PicCAM_Click(object sender, EventArgs e)
        {
            if (fomationDGV.SelectedRows.Count == 0)
            {
                return;
            }
            else
            {
                picCAM.Image = GetImage(fomationDGV.SelectedRows[0].Index);
                fomationDGV.Rows[fomationDGV.SelectedRows[0].Index].ReadOnly = true;
                fomationDGV.SelectedRows[0].Selected = false;
            }
        }

        private void PicLW_Click(object sender, EventArgs e)
        {
            if (fomationDGV.SelectedRows.Count == 0)
            {
                return;
            }
            else
            {
                picLW.Image = GetImage(fomationDGV.SelectedRows[0].Index);
                fomationDGV.Rows[fomationDGV.SelectedRows[0].Index].ReadOnly = true;
                fomationDGV.SelectedRows[0].Selected = false;
            }
        }

        private void PicRDM_Click(object sender, EventArgs e)
        {
            if (fomationDGV.SelectedRows.Count == 0)
            {
                return;
            }
            else
            {
                picRDM.Image = GetImage(fomationDGV.SelectedRows[0].Index);
                fomationDGV.Rows[fomationDGV.SelectedRows[0].Index].ReadOnly = true;
                fomationDGV.SelectedRows[0].Selected = false;
            }
        }

        private void PicLDM_Click(object sender, EventArgs e)
        {
            if (fomationDGV.SelectedRows.Count == 0)
            {
                return;
            }
            else
            {
                picLDM.Image = GetImage(fomationDGV.SelectedRows[0].Index);
                fomationDGV.Rows[fomationDGV.SelectedRows[0].Index].ReadOnly = true;
                fomationDGV.SelectedRows[0].Selected = false;
            }
        }

        private void PicRB_Click(object sender, EventArgs e)
        {
            if (fomationDGV.SelectedRows.Count == 0)
            {
                return;
            }
            else
            {
                picRB.Image = GetImage(fomationDGV.SelectedRows[0].Index);
                fomationDGV.Rows[fomationDGV.SelectedRows[0].Index].ReadOnly = true;
                fomationDGV.SelectedRows[0].Selected = false;
            }
        }
        private void PicCB2_Click(object sender, EventArgs e)
        {
            if (fomationDGV.SelectedRows.Count == 0)
            {
                return;
            }
            else
            {
                picCB2.Image = GetImage(fomationDGV.SelectedRows[0].Index);
                fomationDGV.Rows[fomationDGV.SelectedRows[0].Index].ReadOnly = true;
                fomationDGV.SelectedRows[0].Selected = false;
            }
        }

        private void PicCB1_Click(object sender, EventArgs e)
        {
            if (fomationDGV.SelectedRows.Count == 0)
            {
                return;
            }
            else
            {
                picCB1.Image = GetImage(fomationDGV.SelectedRows[0].Index);
                fomationDGV.Rows[fomationDGV.SelectedRows[0].Index].ReadOnly = true;
                fomationDGV.SelectedRows[0].Selected = false;
            }
        }

        private void PicLB_Click(object sender, EventArgs e)
        {
            if (fomationDGV.SelectedRows.Count == 0)
            {
                return;
            }
            else
            {
                picLB.Image = GetImage(fomationDGV.SelectedRows[0].Index);
                fomationDGV.Rows[fomationDGV.SelectedRows[0].Index].ReadOnly = true;
                fomationDGV.SelectedRows[0].Selected = false;
            }
        }

        private void PicGK_Click(object sender, EventArgs e)
        {
            if (fomationDGV.SelectedRows.Count == 0)
            {
                return;
            }
            else
            {
                picGK.Image = GetImage(fomationDGV.SelectedRows[0].Index);
                fomationDGV.Rows[fomationDGV.SelectedRows[0].Index].ReadOnly = true;
                fomationDGV.SelectedRows[0].Selected = false;
            }
        }
        #endregion

        private Image GetImage(int row)
        {
            Image img = Image.FromFile(imagePath + "Player\\" + fomationDGV.Rows[row].Cells["pImage"].Value.ToString());
            return img;
        }

        private void SelectFomation()
        {
            if (ds.Tables["fomation"] != null)
            {
                ds.Tables["fomation"].Rows.Clear();
                String selectQuery = "SELECT pPosition as '포지션', pName as '이름', pState as '상태', pImage FROM player order by pState;";
                MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, conn);
                adapter.Fill(ds, "fomation");
                fomationDGV.DataSource = ds.Tables["fomation"];
                fomationDGV.Columns["pImage"].Visible = false;
            }
            else
            {
                String selectQuery = "SELECT pPosition as '포지션', pName as '이름', pState as '상태', pImage FROM player order by pState;";
                MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, conn);
                adapter.Fill(ds, "fomation");
                fomationDGV.DataSource = ds.Tables["fomation"];
                fomationDGV.Columns["pImage"].Visible = false;
            }               
        }
        private void SelectPlayers()
        {
            if (ds.Tables["players"] != null)
            {
                ds.Tables["players"].Rows.Clear();
                String selectQuery = "SELECT pId, pState as '상태', pName as '이름', pNum as '등번호', pPosition as '포지션', rPlayed as '경기수', rGoal as '득점', rAssist as '도움' FROM playerrecord, player where playerrecord.rId = player.pId order by pId";
                MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, conn);
                adapter.Fill(ds, "players");
                playersDGV.DataSource = ds.Tables["players"];
            }
            else
            {
                String selectQuery = "SELECT pId, pState as '상태', pName as '이름', pNum as '등번호', pPosition as '포지션', rPlayed as '경기수', rGoal as '득점', rAssist as '도움' FROM playerrecord, player where playerrecord.rId = player.pId order by pId";
                MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, conn);
                adapter.Fill(ds, "players");
                playersDGV.DataSource = ds.Tables["players"];
            }           
        }

        private void PlayersDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                int pId = (int)playersDGV.Rows[e.RowIndex].Cells[0].Value;
                if (ds.Tables["playerStat"] != null)
                {
                    ds.Tables["playerStat"].Rows.Clear();
                }
                pnPlayerL.SendToBack();
                String selectQuery = "SELECT * FROM playerstat, player where playerstat.sId = player.pId and pId = " + pId + "";
                MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, conn);
                adapter.Fill(ds, "playerStat");
                //선수ID
                lbID.Text = ds.Tables["playerStat"].Rows[0]["sId"].ToString();
                // 이미지
                picPlayer.Image = Image.FromFile(imagePath + "Player\\" + ds.Tables["playerStat"].Rows[0]["pImage"].ToString());
                // 키
                lbHeight.Text = ds.Tables["playerStat"].Rows[0]["pHeight"] + " cm";
                // 몸무게
                lbWeight.Text = ds.Tables["playerStat"].Rows[0]["pWeight"] + " kg";
                // 이름pName
                lbName.Text = ds.Tables["playerStat"].Rows[0]["pName"] + "";
                // 나이 계산
                int nowYear = DateTime.Now.Year;
                DateTime age = (DateTime)ds.Tables["playerStat"].Rows[0]["pAge"];
                lbAge.Text = (nowYear - age.Year).ToString() + "세";
                // 등번호
                lbBackNum.Text = "no." + ds.Tables["playerStat"].Rows[0]["pNum"];
                // 국적
                lbNat.Text = ds.Tables["playerStat"].Rows[0]["pNat"] + "";
                //탭1,2,3
                InitTab1();
                InitTab2(pId);
                InitTab3();
            }
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            StringBuilder delestSql = new StringBuilder();
            delestSql.Append("delete from playerstat where sId = " + lbID.Text + ";");
            delestSql.Append("delete from playerrecord where rId = " + lbID.Text + ";");
            delestSql.Append("delete from player where pId = " + lbID.Text + ";");
            conn.Open();
            MySqlCommand command = new MySqlCommand(delestSql.ToString(), conn);
            command.ExecuteNonQuery();
            MessageBox.Show("삭제되었습니다");
            conn.Close();
            pnPlayerD.SendToBack();
            SelectPlayers();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            StringBuilder updateSql = new StringBuilder();
            updateSql.Append("update playerstat set");
            if (ds.Tables["playerStat"].Rows[0]["pPosition"].Equals("GK"))
            {
                updateSql.Append(" GK_Handling = " + txtSkill.Text + ", ");
                updateSql.Append(" GK_Reflexes = " + txtFinish.Text + ", ");
                updateSql.Append(" GK_Kick = " + txtDribble.Text + ", ");
                updateSql.Append(" GK_oneOnOnes = " + txtMark.Text + ", ");
            }
            else
            {
                updateSql.Append(" skill = " + txtSkill.Text + ", ");
                updateSql.Append(" finish = " + txtFinish.Text + ", ");
                updateSql.Append(" dribbling = " + txtDribble.Text + ", ");
                updateSql.Append(" marking = " + txtMark.Text + ", ");
            }
            updateSql.Append(" pass = " + txtPass.Text + ", ");
            updateSql.Append(" firstTouch = " + txtFirstTouch.Text + ", ");
            updateSql.Append(" corners = " + txtCorner.Text + ", ");
            updateSql.Append(" crossing = " + txtCross.Text + ", ");
            updateSql.Append(" freeKick = " + txtFreekick.Text + ", ");
            updateSql.Append(" penaltyKick = " + txtPenalty.Text + ", ");
            updateSql.Append(" ofTheBall = " + txtOfTheBall.Text + ", ");
            updateSql.Append(" vision = " + txtVision.Text + ", ");
            updateSql.Append(" strength = " + txtStrength.Text + ", ");
            updateSql.Append(" pace = " + txtPace.Text + ", ");
            updateSql.Append(" stamina = " + txtStamina.Text + ", ");
            updateSql.Append(" leadership = " + txtLeadership.Text + ", ");
            updateSql.Append(" heading = " + txtHeading.Text + ", ");
            updateSql.Append(" communication = " + txtCommu.Text + ", ");
            updateSql.Append(" positioning = " + txtPositioning.Text);
            updateSql.Append(" where sId = " + lbID.Text + ";");

            updateSql.Append("update player set pReport = '");
            updateSql.Append(txtReport.Text + "'");
            updateSql.Append("where pId = " + lbID.Text);
            conn.Open();
            MySqlCommand command = new MySqlCommand(updateSql.ToString(), conn);
            command.ExecuteNonQuery();
            MessageBox.Show("수정되었습니다");
            conn.Close();
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            InsertForm insertForm = new InsertForm();
            if(insertForm.ShowDialog() == DialogResult.OK)
            {
                SelectPlayers();
            }
        }

        private void TxtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtSearch.Text.Equals(""))
                {
                    playersDGV.DataSource = ds.Tables["players"];
                    MessageBox.Show("공백은 검색할 수 없습니다");
                }
                else
                {
                    DataTable dt;
                    dt = ds.Tables["players"].Copy();
                    dt.Rows.Clear();
                    foreach (DataRow row in ds.Tables["players"].Rows)
                    {
                        foreach (object item in row.ItemArray)
                        {
                            if (item.Equals(txtSearch.Text))
                            {
                                dt.ImportRow(row);
                                break;
                            }
                        }
                    }
                    playersDGV.DataSource = dt;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("해당 선수가 존재하지 않습니다");
                    }
                }
            }
        }

        private void LbBack_Click(object sender, EventArgs e)
        {
            SelectPlayers();
            pnPlayerL.BringToFront();
        }
        private void InitTab3()
        {
            playerCkList.Items.Clear();
            int i = 0;
            foreach (DataRow dataRow in ds.Tables["players"].Rows)
            {
                playerCkList.Items.Add(dataRow["이름"].ToString());
                if (dataRow["이름"].ToString().Equals(lbName.Text))
                {
                    playerCkList.SetItemChecked(i, true);
                }
                i++;
            }
            cbChartType.Items.Clear();
            cbChartType.Items.Add("Rader");
            cbChartType.Items.Add("Bar");
            cbChartType.Items.Add("Area");
            cbChartType.Items.Add("Column");

            btnComparison.Click += BtnComparison_Click;          
        }

        private void BtnComparison_Click(object sender, EventArgs e)
        {
            chart.Series.Clear();
            StringBuilder chartSql = new StringBuilder();
            chartSql.Append("SELECT * FROM playermgtdb.playerstat, player where playerstat.sId = player.pId and pName in (");
           
            for (int i = 0; i < playerCkList.CheckedItems.Count; i++)
            {
                chartSql.Append("'" + playerCkList.CheckedItems[i].ToString() + "', ");
            }
            chartSql.Remove(chartSql.Length -2, 2);
            chartSql.Append(")");
            MySqlDataAdapter adapter = new MySqlDataAdapter(chartSql.ToString(), conn);
            adapter.Fill(ds, "chart");

            Series[] series = new Series[playerCkList.CheckedItems.Count];
            for (int j = 0; j < playerCkList.CheckedItems.Count; j++)
            {
                series[j] = new Series(playerCkList.CheckedItems[j].ToString());
                series[j].Points.AddXY("개인기", ds.Tables["chart"].Rows[j]["skill"]);
                series[j].Points.AddXY("골결정력", ds.Tables["chart"].Rows[j]["finish"]);
                series[j].Points.AddXY("드리블", ds.Tables["chart"].Rows[j]["dribbling"]);
                series[j].Points.AddXY("패스", ds.Tables["chart"].Rows[j]["pass"]);
                series[j].Points.AddXY("퍼스트터치", ds.Tables["chart"].Rows[j]["firstTouch"]);
                series[j].Points.AddXY("코너킥", ds.Tables["chart"].Rows[j]["corners"]);
                series[j].Points.AddXY("크로스", ds.Tables["chart"].Rows[j]["crossing"]);
                series[j].Points.AddXY("프리킥", ds.Tables["chart"].Rows[j]["freeKick"]);
                series[j].Points.AddXY("페널리킥", ds.Tables["chart"].Rows[j]["penaltyKick"]);
                series[j].Points.AddXY("헤딩", ds.Tables["chart"].Rows[j]["heading"]);
                series[j].Points.AddXY("일대일마크", ds.Tables["chart"].Rows[j]["marking"]);
                series[j].Points.AddXY("오프더볼", ds.Tables["chart"].Rows[j]["ofTheBall"]);
                series[j].Points.AddXY("시야", ds.Tables["chart"].Rows[j]["vision"]);
                series[j].Points.AddXY("리더십", ds.Tables["chart"].Rows[j]["leadership"]);
                series[j].Points.AddXY("소통능력", ds.Tables["chart"].Rows[j]["communication"]);
                series[j].Points.AddXY("위치선정", ds.Tables["chart"].Rows[j]["positioning"]);
                series[j].Points.AddXY("몸싸움", ds.Tables["chart"].Rows[j]["strength"]);
                series[j].Points.AddXY("주력", ds.Tables["chart"].Rows[j]["pace"]);
                series[j].Points.AddXY("체력", ds.Tables["chart"].Rows[j]["stamina"]);
                series[j].Points.AddXY("GK핸들링", ds.Tables["chart"].Rows[j]["GK_Handling"]);
                series[j].Points.AddXY("GK반사신경", ds.Tables["chart"].Rows[j]["GK_Reflexes"]);
                series[j].Points.AddXY("GK골킥", ds.Tables["chart"].Rows[j]["GK_Kick"]);
                series[j].Points.AddXY("GK일대일", ds.Tables["chart"].Rows[j]["GK_oneOnOnes"]);

                if (cbChartType.Text.Equals("Column"))
                {
                    series[j].ChartType = SeriesChartType.Column;
                }
                else if (cbChartType.Text.Equals("Bar"))
                {
                    series[j].ChartType = SeriesChartType.Bar;
                }
                else if (cbChartType.Text.Equals("Area"))
                {
                    series[j].ChartType = SeriesChartType.Area;
                }
                else
                {
                    series[j].ChartType = SeriesChartType.Radar;
                }
                chart.Series.Add(series[j]);
            }
        }

        private void InitTab2(int pId)
        {

            String recordSql = "Select pName as '이름', rPlayed as '경기수', rGoal as '골', rAssist as '도움', rShots as '슈팅', rShotsT as '유효슈팅', rfoul as '파울', rYellow as '경고', rRed as '퇴장', rCornerKick as '코너킥', rFleeKick as '프리킥', rCatch as 'GK캐치', rPunch as 'GK펀치', rAerialBall as 'GK공중볼' from playerrecord, player where playerrecord.rId = player.pId and pId = " + pId;
            MySqlDataAdapter adapter = new MySqlDataAdapter(recordSql, conn);
            if (ds.Tables["record"] != null)
            {
                ds.Tables["record"].Rows.Clear(); 
            }
            adapter.Fill(ds, "record");
            playerRecordDGV.DataSource = ds.Tables["record"];
        }
        private void InitTab1()
        {
            if (ds.Tables["playerStat"].Rows[0]["pPosition"].Equals("GK"))
            {
                lbSkill.Text = "GK핸들링";
                txtSkill.Text = ds.Tables["playerStat"].Rows[0]["GK_Handling"].ToString();
                lbFinish.Text = "GK반사신경";
                txtFinish.Text = ds.Tables["playerStat"].Rows[0]["GK_Reflexes"].ToString();
                lbDribble.Text = "GK골킥";
                txtDribble.Text = ds.Tables["playerStat"].Rows[0]["GK_Kick"].ToString();
                lbMark.Text = "GK일대일";
                txtMark.Text = ds.Tables["playerStat"].Rows[0]["GK_oneOnOnes"].ToString();
            }
            else
            {
                lbSkill.Text = "개인기";
                txtSkill.Text = ds.Tables["playerStat"].Rows[0]["skill"].ToString();
                lbFinish.Text = "골결정력";
                txtFinish.Text = ds.Tables["playerStat"].Rows[0]["finish"].ToString();
                lbDribble.Text = "드리블";
                txtDribble.Text = ds.Tables["playerStat"].Rows[0]["dribbling"].ToString();
                lbMark.Text = "일대일마크";
                txtMark.Text = ds.Tables["playerStat"].Rows[0]["marking"].ToString();
            }
            txtFirstTouch.Text = ds.Tables["playerStat"].Rows[0]["firstTouch"].ToString();
            txtCorner.Text = ds.Tables["playerStat"].Rows[0]["corners"].ToString();
            txtCross.Text = ds.Tables["playerStat"].Rows[0]["crossing"].ToString();
            txtFreekick.Text = ds.Tables["playerStat"].Rows[0]["freeKick"].ToString();
            txtPenalty.Text = ds.Tables["playerStat"].Rows[0]["penaltyKick"].ToString();
            txtHeading.Text = ds.Tables["playerStat"].Rows[0]["heading"].ToString();
            txtPass.Text = ds.Tables["playerStat"].Rows[0]["pass"].ToString();
            txtOfTheBall.Text = ds.Tables["playerStat"].Rows[0]["ofTheBall"].ToString();
            txtVision.Text = ds.Tables["playerStat"].Rows[0]["vision"].ToString();
            txtLeadership.Text = ds.Tables["playerStat"].Rows[0]["leadership"].ToString();
            txtCommu.Text = ds.Tables["playerStat"].Rows[0]["communication"].ToString();
            txtPositioning.Text = ds.Tables["playerStat"].Rows[0]["positioning"].ToString();
            txtStrength.Text = ds.Tables["playerStat"].Rows[0]["strength"].ToString();
            txtPace.Text = ds.Tables["playerStat"].Rows[0]["pace"].ToString();
            txtStamina.Text = ds.Tables["playerStat"].Rows[0]["stamina"].ToString();
            txtReport.Text = ds.Tables["playerStat"].Rows[0]["pReport"].ToString();
        }

        private void BtnInfo_Click(object sender, EventArgs e)
        {
            lbTopMenu.Text = btnInfo.Text;
            pnPlayer.Visible = false;
            pnHome.Visible = false;
            pnInfo.Visible = true;
            pnStaff.Visible = false;
            pnFormation.Visible = false;
        }

        private void BtnFomation_Click(object sender, EventArgs e)
        {
            SelectFomation();
            lbTopMenu.Text = btnFomation.Text;
            pnPlayer.Visible = false;
            pnHome.Visible = false;
            pnInfo.Visible = false;
            pnStaff.Visible = false;
            pnFormation.Visible = true;
        }

        private void BtnStaff_Click(object sender, EventArgs e)
        {
            lbTopMenu.Text = btnStaff.Text;
            pnPlayer.Visible = false;
            pnHome.Visible = false;
            pnInfo.Visible = false;
            pnStaff.Visible = true;
            pnFormation.Visible = false;
        }

        private void BtnPlayer_Click(object sender, EventArgs e)
        {
            lbTopMenu.Text = btnPlayer.Text;
            pnPlayer.Visible = true;
            pnPlayerL.BringToFront();
            pnHome.Visible = false;
            pnInfo.Visible = false;
            pnStaff.Visible = false;
            pnFormation.Visible = false;
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            lbTopMenu.Text = btnHome.Text;
            pnPlayer.Visible = false;
            pnHome.Visible = true;
            pnInfo.Visible = false;
            pnStaff.Visible = false;
            pnFormation.Visible = false;
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            this.Close();
        }
    }
}