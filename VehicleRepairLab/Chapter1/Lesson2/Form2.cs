using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace VehicleRepairLab.Chapter1.Lesson2
{
    public partial class Form2 : Form
    {
        
        private readonly string xsdFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VehiclesRepairs.xsd");
        private readonly string xmlFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VehiclesRepairs.xml");

        private DataSet ds;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            PopulateDataSet();
            dgVehicles.DataSource = ds;
            dgVehicles.DataMember = "Vehicles";
            dgRepairs.DataSource = ds;
            dgRepairs.DataMember = "Vehicles.vehicles_repairs";
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
        }

        private void PopulateDataSet()
        {
            if (File.Exists(xsdFile))
            {
                ds = new DataSet();
                ds.ReadXmlSchema(xsdFile);
            }
            else
            {
                CreateSchema();
            }
            if (File.Exists(xmlFile))
            {
                ds.ReadXml(xmlFile, XmlReadMode.IgnoreSchema);
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            ds.WriteXml(xmlFile, XmlWriteMode.DiffGram);
        }

    }
}
