using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PSI_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Button Calculate = new Button();
            Calculate.Click += Calculate_Click;
            String name = NameTbox.Text;
            String age = AgeTbox.Text;

        }
        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            //Try Catch to throw exception when not all the information is given.
            try
            {
                //Variable declarations to get the info from the TextBoxes
                int riskValue = 0;
                int rRate = Int32.Parse(ResRateTbox.Text);
                int bPress = Int32.Parse(BloodPressTbox.Text);
                int temp = Int32.Parse(TempTbox.Text);
                int pulse = Int32.Parse(PulseTbox.Text);
                int ph = Int32.Parse(pHTbox.Text);
                int bun = Int32.Parse(BUNTbox.Text);
                int sodium = Int32.Parse(SodiumTbox.Text);
                int glucose = Int32.Parse(GlucoseTbox.Text);
                int hematocrit = Int32.Parse(HematocritTbox.Text);
                int oxygen = Int32.Parse(OxygenPressTbox.Text);

                /*Variable declarations for the Radio Button info.
                 *The data is changed in another scope, but used here.
                 *This is why they are declared here and not later.*/
                String nhres = "0";
                String neoplatic = "0";
                String liver = "0";
                String heartfail = "0";
                String cerebro = "0";
                String renal = "0";
                String ams = "0";
                String pe = "0";

                //Long list of if statements to determine the risk value of a patient.
                if (Female.IsChecked == true)
                {
                    riskValue -= 10;
                }
                if (rRate >= 30)
                {
                    riskValue += 20;
                }
                if (bPress < 90)
                {
                    riskValue += 20;
                }
                // Requires both C and F
                /*Has a different if statement for Celsius and Fahrenheit values.
                 *Celsius is the default and Fahrenheit is converted to Celsius
                 *to later be written into a file.*/
                if ((temp < 35 || temp > 39.9) && Celsius.IsChecked == true)
                {
                    riskValue += 15;
                }
                if ((temp < 95 || temp > 103.8) && Fahrenheit.IsChecked == true)
                {
                    riskValue += 15;
                }
                if (pulse >= 125)
                {
                    riskValue += 10;
                }
                if (ph < 7.35)
                {
                    riskValue += 30;
                }
                /*Has a different if statement for mg/dl and mmol/L values.
                 *mg/dl is the default and mmol is converted to mg/dl
                 *to later be written into a file.*/
                if (bun >= 30 && BUN_mg.IsChecked == true)
                {
                    riskValue += 20;
                }
                if (bun >= 11 && BUN_mmol.IsChecked == true)
                {
                    riskValue += 20;
                }
                if (sodium < 130)
                {
                    riskValue += 20;
                }
                /*Has a different if statement for mg/dl and mmol/L values.
                 *mg/dl is the default and mmol is converted to mg/dl
                 *to later be written into a file.*/
                if (glucose >= 250 && Glucose_mg.IsChecked == true)
                {
                    riskValue += 10;
                }
                if (glucose >= 14 && Glucose_mmol.IsChecked == true)
                {
                    riskValue += 10;
                }
                if (hematocrit < 30)
                {
                    riskValue += 10;
                }
                /*Has a different if statement for mmHg and kPa values.
                 *mmHg is the default and mmol is converted to kPa
                 *to later be written into a file.*/
                if (oxygen < 60 && Oxygen_mmHg.IsChecked == true)
                {
                    riskValue += 10;
                }
                if (oxygen < 8 && Oxygen_kPa.IsChecked == true)
                {
                    riskValue += 10;
                }
                if (NHRes.IsChecked == true)
                {
                    nhres = "1";
                    riskValue += 10;
                }
                if (Neoplastic.IsChecked == true)
                {
                    neoplatic = "1";
                    riskValue += 30;
                }
                if (Liver.IsChecked == true)
                {
                    liver = "1";
                    riskValue += 20;
                }
                if (HeartFail.IsChecked == true)
                {
                    heartfail = "1";
                    riskValue += 10;
                }
                if (Cerebro.IsChecked == true)
                {
                    cerebro = "1";
                    riskValue += 10;
                }
                if (Renal.IsChecked == true)
                {
                    renal = "1";
                    riskValue += 10;
                }
                if (AMS.IsChecked == true)
                {
                    ams = "1";
                    riskValue += 20;
                }
                if (PE.IsChecked == true)
                {
                    pe = "1";
                    riskValue += 10;
                }
                String rv = riskValue.ToString();
                MessageBox.Show(rv);
                riskValue += Int32.Parse(AgeTbox.Text);

                //If statements that determine risk class and then print out the 
                //risk class and Admission recommendation.
                if (riskValue == Int32.Parse(AgeTbox.Text))
                {
                    MessageBox.Show("Patient is in Risk Class I. \n Outpatient Care Recommended.");
                }
                else
                {
                    if (riskValue <= 70 && riskValue != 10)
                    {
                        MessageBox.Show("Patient is in Risk Class II. \n Outpatient Care Recommended");
                    }
                    if (riskValue > 70 && riskValue <= 90)
                    {
                        MessageBox.Show("Patient is in Risk Class III. \n Outpatient Care or Observation Admission Recommended.");
                    }
                    if (riskValue > 90 && riskValue <= 130)
                    {
                        MessageBox.Show("Patient is in Risk Class IV. \n Inpatient Admission Recommended.");
                    }
                    if (riskValue > 130)
                    {
                        MessageBox.Show("Patient is in Risk Class V. \n Inpatient Admission Recommended. \n Check patient for sepsis");
                    }
                }

                //Conversions to the Standard units of each type that has one.
                if (Fahrenheit.IsChecked == true)
                {
                    temp = Convert.ToInt32((temp - 32) * 5 / 9);
                }
                if (BUN_mmol.IsChecked == true)
                {
                    bun = Convert.ToInt32(bun / 0.357);
                }
                if (Glucose_mmol.IsChecked == true)
                {
                    glucose = Convert.ToInt32(glucose / 0.056);
                }
                if (Oxygen_kPa.IsChecked == true) 
                { 
                    oxygen = Convert.ToInt32(oxygen * 7.5006156130264);
                }

                //Declaring String variables for the entry into the data.csv file
                String entry = "";
                String sex = "";
                if (Male.IsChecked == true)
                {
                    sex = "Male";
                }
                if (Female.IsChecked == true)
                {
                    sex = "Female";
                }

                //Long list of appending all of the patient's information.
                entry = (", "+NameTbox.Text+"," +AgeTbox.Text+ ", " + sex);
                entry = (entry + ", " + nhres + ", " + neoplatic + ", ");
                entry = (entry + liver + ", " + heartfail + ", ");
                entry = (entry + cerebro + ", " + renal + ", ");
                entry = (entry + ams + ", " + pe + ", ");
                entry = (entry + rRate.ToString() + ", " + bPress.ToString() + ", ");
                entry = (entry + temp.ToString() +" Celsius"+", " + pulse.ToString() + ", ");
                entry = (entry + ph.ToString() + ", " + bun.ToString() + " mg/dl" + ", ");
                entry = (entry + sodium.ToString() + ", " + glucose.ToString() + " mg/dl" + ", ");
                entry = (entry + hematocrit.ToString() + ", " + oxygen.ToString()+" mmHg");

                //Checks the length of the data.csv file and then appends a new 
                if (new FileInfo("data.csv").Length > 2)
                {
                    int ID = 0;
                    ID += File.ReadLines("data.csv").Count();
                    String id = ID.ToString();
                    String line = (id + "." + entry);
                    using (StreamWriter sw = File.AppendText("data.csv"))
                    {
                        sw.WriteLine(line);
                    }
                    MessageBox.Show("Data added to the end of data.csv");
                }
                /*Had some problems with File and FileInfo always registering the data.csv
                 *as length of 1 even when the file is empty. Tried using CsvReader with the opposite result.
                 *In the end I had to add a Patient List: title in order for the program to append things correctly*/
                if (new FileInfo("data.csv").Length == 2)
                {
                    using (StreamWriter sw = File.CreateText("data.csv"))
                    {
                        sw.WriteLine("Patient List:");
                        sw.WriteLine("1." + entry);
                    }
                    MessageBox.Show("Data added to the end of data.csv");
                }

                riskValue = 0;
            }
            catch (Exception input)
            {
                MessageBox.Show("Please enter in all information");
            }
        }
    }
}
