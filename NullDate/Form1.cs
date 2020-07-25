using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NullDate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tblDemoBindingSource.EndEdit();
            //dataGridView1.EndEdit();

            try
            {
                this.tableAdapterManager1.UpdateAll(this.nullDataDataSet1);
                nullDataDataSet1.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.tbl_DemoTableAdapter1.Fill(nullDataDataSet1.tbl_Demo);
            this.dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dataGridView1_DataError);

        }
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

            DataGridView dgv = sender as DataGridView;

            if (dgv.DataSource is BindingSource)
            {
                BindingSource bindSrc = dgv.DataSource as BindingSource;
                if (e.RowIndex > bindSrc.Count)
                {
                    //Ignore the error that the current row of the DataGridView will be deleted.
                    return;
                }
            }
            MessageBox.Show(e.Exception.Message);
        }



        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (tblDemoBindingSource.Current == null) return;
            if (MessageBox.Show("Are you sure to delete?", this.Text,
                MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                tblDemoBindingSource.RemoveCurrent();
                tblDemoBindingSource.EndEdit();
            }
        }


 
    }
}
