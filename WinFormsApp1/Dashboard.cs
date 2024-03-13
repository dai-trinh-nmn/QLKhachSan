using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WinFormsApp1
{
    public partial class Dashboard : Form
    {
        private string username, role;
        string? query;
        function fn = new function();

        public Dashboard(string username, string role)
        {
            InitializeComponent();
            this.username = username;
            this.role = role;

            if (role != "admin")
            {
                tabControl1.SelectedTab = tabControl1.TabPages[1];
                roomRegistration(username);
            }
            else if (role == "admin")
            {
                tabControl1.SelectedTab = tabControl1.TabPages[0];
                addRoom(username);
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (role != "admin" && (e.TabPageIndex == 0 || e.TabPageIndex == 3 || e.TabPageIndex == 4))
            {
                e.Cancel = true;
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentTabIndex = tabControl1.SelectedIndex;

            if (tabControl1.SelectedTab == tabControl1.TabPages[0])
            {
                addRoom(username);
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages[1])
            {
                roomRegistration(username);
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages[2])
            {
                checkOut(username);
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages[3])
            {
                customerManagement(username);
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages[4])
            {
                staffManagement(username);
            }
        }

        //TAB THEM PHONG
        public void addRoom(string username)
        {
            lbAddRoomWelcome.Text = "Xin chào " + username + "!";
            btnRoomEdit.Enabled = false;
            btnRoomDelete.Enabled = false;
            query = "SELECT * FROM rooms";
            DataSet ds = fn.getData(query);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            if (tbRoomNo.Text != null && cbRoomType.Text != null && cbBedNum.Text != null && tbRoomPrice.Text != null)
            {
                string roomNo = tbRoomNo.Text;
                string roomType = cbRoomType.Text;
                string bedNum = cbBedNum.Text;
                Int64 price;
                bool isNumPrice = Int64.TryParse(tbRoomPrice.Text, out price);
                if (isNumPrice)
                {
                    query = "insert into rooms (roomNo, roomType, bedNum, price) values ('" + roomNo + "', N'" + roomType + "', '" + bedNum + "', " + price + ")";
                    fn.setData(query, "Đã thêm phòng!");
                    addRoomClearAll();
                    addRoom(username);
                } else
                {
                    MessageBox.Show("Vui lòng nhập đúng giá phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            tbRoomNo.Text = row.Cells["roomNo"].Value.ToString();
            cbRoomType.Text = row.Cells["roomType"].Value.ToString();
            cbBedNum.Text = row.Cells["bedNum"].Value.ToString();
            tbRoomPrice.Text = row.Cells["price"].Value.ToString();
            btnRoomDelete.Enabled = true;
            btnRoomEdit.Enabled = true;
        }

        private void btnRoomEdit_Click(object sender, EventArgs e)
        {
            if (tbRoomNo.Text != null && cbRoomType.Text != null && cbBedNum.Text != null && tbRoomPrice.Text != null)
            {
                string roomNo = tbRoomNo.Text;
                string roomType = cbRoomType.Text;
                string bedNum = cbBedNum.Text;
                Int64 price;
                bool isNumPrice = Int64.TryParse(tbRoomPrice.Text, out price);
                if (isNumPrice)
                {
                    query = "UPDATE rooms SET roomType = N'" + roomType + "', bedNum = '" + bedNum + "', price = " + price + " WHERE roomNo = '" + roomNo + "'";
                    fn.setData(query, "Đã cập nhật thông tin!");
                    addRoomClearAll();
                    addRoom(username);
                } else
                {
                    MessageBox.Show("Vui lòng nhập đúng giá phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRoomDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xoá phòng này?", "Xác nhận!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                query = $"delete from rooms where roomNo = '{tbRoomNo.Text}'";
                fn.setData(query, "Phòng đã được xoá!");
            }
            addRoom(username);
        }

        public void addRoomClearAll()
        {
            tbRoomNo.Clear();
            cbRoomType.SelectedIndex = -1;
            cbBedNum.SelectedIndex = -1;
            tbRoomPrice.Clear();
        }

        //TAB DANG KY THUE PHONG
        public void roomRegistration(string username)
        {
            lbRoomRegistrationWelcome.Text = "Xin chào " + username + "!";

        }

        //TAB THANH TOAN
        public void checkOut(string username)
        {
            lbCheckOutWelcome.Text = "Xin chào " + username + "!";
        }

        //TAB QUAN LY KHACH HANG
        public void customerManagement(string username)
        {

        }

        //TAB NHAN VIEN
        public void staffManagement(string username)
        {

        }
    }
}