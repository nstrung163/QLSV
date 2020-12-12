using Nhom1_12_1_2020.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom1_12_1_2020.GUI
{
    public partial class frmSinhVienChiTiet : Form
    {
        Student sinhVien;

        public frmSinhVienChiTiet()
        {
            InitializeComponent();
            this.Text = "Thêm mới sinh viên";
        }

        public frmSinhVienChiTiet(Student sinhVien)
        {
            InitializeComponent();
            this.Text = "Chỉnh sửa nhân viên";
            this.sinhVien = sinhVien;
            txtMaSV.Text = this.sinhVien.ID;
            txtHoDem.Text = this.sinhVien.FirstName;
            txtTen.Text = this.sinhVien.LastName;
            //Parse DateTime to String
            DateTime dateOfBirth = (DateTime) this.sinhVien.DateOfBirth;
            String DOBstr = dateOfBirth.ToString();
            txtNgaySinh.Text = DOBstr;
            txtNoiSinh.Text = this.sinhVien.PlaceOfBirth;
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
            var maSv = txtMaSV.Text;
            var hoDem = txtHoDem.Text;
            var ten = txtTen.Text;
            DateTime ngaySinh = DateTime.Parse(txtNgaySinh.Text);
            var noiSinh = txtNoiSinh.Text;
            int gioiTinh = (int) txtGioiTinh.Value;
            
            if ( sinhVien == null)
            {
                // Thêm mới sinh viên
                var student = new Student
                {
                    ID = maSv,
                    FirstName = hoDem,
                    LastName = ten,
                    DateOfBirth = ngaySinh,
                    PlaceOfBirth = noiSinh,
                    Gender = gioiTinh,
                    IDClassroom = "1"
                };
                AppQLSVDBContext db = new AppQLSVDBContext();
                db.Students.Add(student);
                db.SaveChanges();
                DialogResult = DialogResult.OK;
            } else
            {
                var db = new AppQLSVDBContext();
                var sv = db.Students.Where(c => c.ID == sinhVien.ID).FirstOrDefault(); // Truy xuất sinh viên đang chọn dưới DB
                sv.ID = maSv; // chỉnh sửa id ở DB thành txtMaSV
                sv.FirstName = hoDem;
                sv.LastName = ten;
                sv.DateOfBirth = ngaySinh;
                sv.PlaceOfBirth = noiSinh;
                sv.Gender = gioiTinh;
                // Lưu dữ liệu xuống DB
                db.SaveChanges();
                DialogResult = DialogResult.OK;
            }

        }

        private void frmSinhVienChiTiet_Load(object sender, EventArgs e)
        {
            //Set value for domainUpdown
            DomainUpDown.DomainUpDownItemCollection collection = this.domainLopHoc.Items;
            var db = new AppQLSVDBContext();
            collection.Add("TinK41A");
            collection.Add("TinK41B");
            collection.Add("TinK41C");
            collection.Add("TinK41D");
            collection.Add("TinK41E");
        }
    }
}
