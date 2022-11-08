using System;
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
using System.Windows.Shapes;
using System.Windows.Forms.Integration;
using Microsoft.Reporting.WinForms;
using System.Data;

namespace Exam_System
{
    /// <summary>
    /// Interaction logic for reports_Form.xaml
    /// </summary>
    public partial class reports_Form : Window
    {
        string ServerName = "http://localhost/ReportServer";
        string Report1_Path = "/Examination system(Project)/Report_1_StInDep";
        string Report2_Path = "/Examination system(Project)/Report_2_St_Grade";
        string Report3_Path = "/Examination system(Project)/Report_3_StPerCrsForIns";
        string Report4_Path = "/Examination system(Project)/Report_4_TopicName";
        string Report5_Path = "/Examination system(Project)/Report_5_Exam_Q";
        string Report6_Path = "/Examination system(Project)/Report_6_St_Ex_answer";

        ReportViewer reportViewer = new ReportViewer();
        public reports_Form()
        {
            InitializeComponent();            
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ServerName = "http://localhost/ReportServer";
            winForm_Host.Child = reportViewer;
            reportViewer.ProcessingMode = ProcessingMode.Remote;
            reportViewer.ServerReport.ReportServerUrl = new Uri(ServerName);
        }

        private void report1_Btn_Click(object sender, RoutedEventArgs e)
        {            
            reportViewer.ServerReport.ReportPath = Report1_Path;
            reportViewer.RefreshReport();
        }
        private void report2_Btn_Click(object sender, RoutedEventArgs e)
        {
            reportViewer.ServerReport.ReportPath = Report2_Path;
            reportViewer.RefreshReport();
        }
        private void report3_Btn_Click(object sender, RoutedEventArgs e)
        {
            reportViewer.ServerReport.ReportPath = Report3_Path;
            reportViewer.RefreshReport();
        }
        private void report4_Btn_Click(object sender, RoutedEventArgs e)
        {
            reportViewer.ServerReport.ReportPath = Report4_Path;
            reportViewer.RefreshReport();
        }
        private void report5_Btn_Click(object sender, RoutedEventArgs e)
        {
            reportViewer.ServerReport.ReportPath = Report5_Path;
            reportViewer.RefreshReport();

        }
        private void report6_Btn_Click(object sender, RoutedEventArgs e)
        {
            reportViewer.ServerReport.ReportPath = Report6_Path;
            reportViewer.RefreshReport();
        }

    }
}
