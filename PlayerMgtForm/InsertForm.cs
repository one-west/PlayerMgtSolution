using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace PlayerMgtForm
{
    public partial class InsertForm : Form
    {
        private static String connStr = "Server=localhost;Database=playermgtdb;Uid=root;Pwd=1234";
        MySqlConnection conn = null;
        String savePath = "";
        public InsertForm()
        {
            InitializeComponent();
            InitComboBox();
            btnInsert.Click += BtnInsert_Click;
            btnFile.Click += BtnFile_Click;
           
        }

        private void BtnFile_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "(*.png)(*.jpg)|*.png;*.jpg";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                lbImage.Text = openFileDialog.SafeFileName;
                savePath = openFileDialog.FileName;

            }
            openFileDialog.Dispose();
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("입력하시겠습니까?", "입력", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Bitmap image = new Bitmap(savePath);
                    String saveRoute = "D:\\daelim\\2023\\C#\\PlayerMgtSolution\\PlayerMgtForm\\resources\\Player\\";
                    if (image != null)
                    {
                        image.Save(saveRoute + lbImage.Text);
                        image.Dispose();
                    }
                    conn = new MySqlConnection(connStr);
                    conn.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.Append("insert into player (pState, pName, pPosition, pNum, pNat, pHeight, pWeight, pAge, pImage) ");
                    sql.Append("value ('" + cbState.Text + "', ");
                    sql.Append("'" + txtName.Text + "', ");
                    sql.Append("'" + txtPosition.Text + "', ");
                    sql.Append(txtNum.Text + ", ");
                    sql.Append("'" + txtNat.Text + "', ");
                    sql.Append(txtHeight.Text + ", ");
                    sql.Append(txtWeight.Text + ", ");
                    sql.Append("'" + txtAge.Text + "', ");
                    sql.Append("'" + lbImage.Text + "');");
                    sql.Append("insert into playerrecord (rPlayed, rGoal, rAssist, rShots, rShotsT, rfoul, rYellow, rRed, rCornerKick, rFleeKick, rCatch, rPunch, rAerialBall) value(0,0,0,0,0,0,0,0,0,0,0,0,0);");
                    sql.Append("insert into playerstat (skill, finish, dribbling, pass, firstTouch, corners, crossing, freeKick, penaltyKick, ofTheBall, vision, strength, pace, stamina, leadership, heading, communication, marking, positioning, GK_Handling, GK_Reflexes, GK_Kick, GK_oneOnOnes) values (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);");

                    MySqlCommand command = new MySqlCommand(sql.ToString(), conn);
                    command.ExecuteNonQuery();
                    MessageBox.Show("입력되었습니다");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    this.Close();
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("잘못된 데이터 입력입니다 \n" + ex.InnerException);
            }
        }

        private void InitComboBox()
        {
            cbState.Items.Clear();
            cbState.Items.Add("선발");
            cbState.Items.Add("후보");
            cbState.Items.Add("부상");
            cbState.Items.Add("임대");
        }
    }
}
