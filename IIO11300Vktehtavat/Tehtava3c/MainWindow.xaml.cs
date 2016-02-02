using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tehtava3C
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtPath.Text = ConfigurationManager.AppSettings["DirPath"].ToString();

        }


        private void btnGetFiles_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DirectoryInfo dinfo = new DirectoryInfo(txtPath.Text);
                FileInfo[] files = dinfo.GetFiles("*.txt"); //.txt tiedostot syyniin
                foreach (FileInfo file in files)
                {
                    lbFiles.Items.Add(file.Name);  //lisätään tiedoston nimet listboxiin
                }
                

            }
            catch (Exception ex)
            {
                tbNotification.Text = "Something wrong with the path?";
            }
        }



        private void btnCombine_Click(object sender, RoutedEventArgs e)
        {
            string path = txtPath.Text;
            StringBuilder sb = new StringBuilder();
            try
            {
                for (int i = 0; i < lbFiles.Items.Count; i++)  //käydään kaikki filut läpi
                {

                    sb.Append(File.ReadAllText(path + "\\" + lbFiles.Items[i].ToString())); //appendataaan stringiin sb readalltext-komennolla kaikki

                }
            }
            catch (Exception ex)
            {
                tbNotification.Text = "File read failed";
            }

            try
            {

                File.WriteAllText(txtTargetFile.Text, sb.ToString()); //äärimmäisen kätevä writealltext tekee stringistä tekstitiedoston
                tbNotification.Text = "File created succesfully!!!";
            }
            catch (Exception ex)
            {
                tbNotification.Text = "Check the target path ";
            }
        }
    }
}
