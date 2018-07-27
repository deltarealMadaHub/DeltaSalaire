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

namespace DeltaSalaire
{
    /// <summary>
    /// Logique d'interaction pour f_etats.xaml
    /// </summary>
    public partial class f_etats : Window
    {
        utilitaire UTs;
        public string[] mois;
        public string idsal;
        public string ann;
        public bool bavantpaiement = false;
        public f_etats()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            UTs = (this.Owner as MainWindow).UT;
            string tri = "";
            /*Microsoft.Reporting.WinForms.ReportDataSource dd = null;
            _reportViewer.LocalReport.ReportPath = "\\fichedepaie.rdlc";
            dd = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1");*/
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
                Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new
                Microsoft.Reporting.WinForms.ReportDataSource();
            reportDataSource1.Name = "DataSet1";
            reportDataSource2.Name = "DataSet2";
            UTs.FichedePaies.Clear();
            string[] smois = mois;// mois.Split(',');
            for (int i = 0; i < smois.Length; i++)
            {
                string mm = smois[i];
                mm = "valeur" + mm;
                UTs.chargerFichedePaie(idsal, ann, mm);
                //payé
                utilitaire.FichedePaie fp = new DeltaSalaire.utilitaire.FichedePaie();
                fp.annee = ann;
                fp.code = "PAYE";
                fp.deduction = "2";
                fp.idsalarie = idsal;
                fp.mois = UTs.listemois[int.Parse(mm.Replace("valeur", "")) - 1];
                fp.nummois = mm.Replace("valeur", "");
                fp.rang = "50";
                fp.taux = "0";
                fp.txtligne = "Montant Payé";
                fp.unite = "Chf";
                if (bavantpaiement)
                    fp.valeur = decimal.Parse( UTs.Grillesalaries.Where(w => w.code == "SALNET").First().valeur);
                else
                    fp.valeur = (GetPaye(mm.Replace("valeur", "")));
                UTs.FichedePaies.Add(fp);
                //date paiement
                fp = new DeltaSalaire.utilitaire.FichedePaie();
                fp.annee = ann;
                fp.code = "DATEPAIE";
                fp.deduction = "2";
                fp.idsalarie = idsal;
                fp.mois = UTs.listemois[int.Parse(mm.Replace("valeur", "")) - 1];
                fp.nummois = mm.Replace("valeur", "");
                fp.rang = "51";
                fp.taux = "0"; 
                fp.txtligne = "Date paiement";
                fp.unite = "";
                fp.valeur = 0;
                if (bavantpaiement)
                    fp.datepaiement = string.Format("{0:dd.MM.yyyy}", DateTime.Now);
                else
                    fp.datepaiement = GetDatePaie(mm.Replace("valeur", ""));
                UTs.FichedePaies.Add(fp);
            }
            reportDataSource1.Value = UTs.FichedePaies;
            //reportDataSource2.Value = UTs.salaries.Where(w => w.idsalaries != "");
            _reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            //_reportViewer.LocalReport.DataSources.Add(reportDataSource2);
            _reportViewer.LocalReport.ReportPath = "FicheSalaire/fichedepaie.rdlc";
            _reportViewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            _reportViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            _reportViewer.ZoomPercent = 100;
            this._reportViewer.RefreshReport();
        }

        private decimal GetPaye(string smois)
        {
            decimal rep = 0;
            //if (smois == "1")
            IEnumerable<utilitaire.PaiementSalaire> ss = UTs.PaiementSalaires.Where(w => w.mois == smois);
            if (ss.Count() > 0)
                rep = (ss.First().valeur);
            else
                rep = 0;
            return rep;
        }

        private string GetDatePaie(string smois)
        {
            string rep = "";
            //if (smois == "1")
            IEnumerable<utilitaire.PaiementSalaire> ss = UTs.PaiementSalaires.Where(w => w.mois == smois);
            if (ss.Count() > 0)
                rep = (ss.First().datepaiement);
            else
                rep = "";
            return rep;
        }
    }
}
