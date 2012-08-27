using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VehicleRepairLab.Chapter1.Lesson1
{
    public partial class Form1 : Form
    {

        private DataSet ds;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateSchema();
        }

        private void CreateSchema()
        {
            ds = new DataSet("VehiclesRepairs");

            var vehicles = ds.Tables.Add("Vehicles");
            vehicles.Columns.Add("VIN", typeof(string));
            vehicles.Columns.Add("Make", typeof(string));
            vehicles.Columns.Add("Model", typeof(string));
            vehicles.Columns.Add("Year", typeof(int));
            vehicles.PrimaryKey = new DataColumn[] { vehicles.Columns["VIN"] };

            var repairs = ds.Tables.Add("Repairs");
            var pk = repairs.Columns.Add("ID", typeof(int));
            pk.AutoIncrement = true;
            pk.AutoIncrementSeed = -1;
            pk.AutoIncrementStep = -1;
            repairs.Columns.Add("VIN", typeof(string));
            repairs.Columns.Add("Description", typeof(string));
            repairs.Columns.Add("Cost", typeof(decimal));
            repairs.PrimaryKey = new DataColumn[] { vehicles.Columns["ID"] };

            ds.Relations.Add("vehicles_repairs", vehicles.Columns["VIN"], repairs.Columns["VIN"]);

            MessageBox.Show("Schema created");
        }

    }
}
