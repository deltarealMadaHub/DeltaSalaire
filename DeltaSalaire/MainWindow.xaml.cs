using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MySql.Data;
using MySql.Data.MySqlClient;
using static DeltaSalaire.App;
using System.Globalization;
using System.Threading;
using System.Xml;
//using System.Windows.Forms;

namespace DeltaSalaire
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public class ItemscountToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (int.Parse(value.ToString()) > 0)
                return (bool)true;
            else
                return (bool)false;
            /*
            switch (value.ToString().ToLower())
            {
                case "yes":
                case "oui":
                    return true;
                case "no":
                case "non":
                    return false;
            }
            return false;*/
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");

        }
    }

    public class CheckMoispaye : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            object dd = Application.Current.FindResource("imoispaye");
            //this.Resources[]
            if (int.Parse(dd.ToString()) >= int.Parse(parameter.ToString()))
                return (bool)true;
            else
                return (bool)false;
            /*
            switch (value.ToString().ToLower())
            {
                case "yes":
                case "oui":
                    return true;
                case "no":
                case "non":
                    return false;
            }
            return false;*/
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");

        }
    }
    public class IsCheckedToSelectionModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (((bool)value) == true)
                return DataGridSelectionMode.Extended;
            else
                return DataGridSelectionMode.Single;
            /*
            switch (value.ToString().ToLower())
            {
                case "yes":
                case "oui":
                    return true;
                case "no":
                case "non":
                    return false;
            }
            return false;*/
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");

        }
    }
    public class PlusMoinsToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.ToString() == "-")
                return (bool)true;
            else
                return (bool)false;
            /*
            switch (value.ToString().ToLower())
            {
                case "yes":
                case "oui":
                    return true;
                case "no":
                case "non":
                    return false;
            }
            return false;*/
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");

        }
    }

    public class DatePickerToShort : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value == "")
                return null;
            else
                return DateTime.Parse(value.ToString()).Date;
            /*
            switch (value.ToString().ToLower())
            {
                case "yes":
                case "oui":
                    return true;
                case "no":
                case "non":
                    return false;
            }
            return false;*/
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value == "")
                return null;
            else
                return DateTime.Parse(value.ToString()).Date.ToShortDateString();
            throw new NotSupportedException("Cannot convert back");

        }
    }
    public class ValueToBooleanConverter : IValueConverter
    {
        public object Convert(Object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((value as TextBox).Text == "1")
                return (bool)true;
            else
                return (bool)false;
            /*
            switch (value.ToString().ToLower())
            {
                case "yes":
                case "oui":
                    return true;
                case "no":
                case "non":
                    return false;
            }
            return false;*/
        }

        public object ConvertBack(Object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");

        }
    }

    public class CurItemPaiementComptabiliseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return false;
            utilitaire.PaiementSalaireEnvoie ff = value as utilitaire.PaiementSalaireEnvoie;
            if (ff.envoye == "")
                return false;
            else
            {
                if (ff.charge == "")
                    return true;
                else
                    return false;
            }
            /*
            switch (value.ToString().ToLower())
            {
                case "yes":
                case "oui":
                    return true;
                case "no":
                case "non":
                    return false;
            }
            return false;*/
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
        }
    }

    public class ItemsContenuConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return "";
            utilitaire.Grillesalarie ff = value as utilitaire.Grillesalarie;
            return ff.code;
            /*
            switch (value.ToString().ToLower())
            {
                case "yes":
                case "oui":
                    return true;
                case "no":
                case "non":
                    return false;
            }
            return false;*/
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "";
        }
    }
    public class PremierPaiementAenvoyer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return false;
            ObservableCollection<utilitaire.PaiementSalaireEnvoie> ff = value as ObservableCollection<utilitaire.PaiementSalaireEnvoie>;
            if (ff.Count() == 0)
                return false;
            if (ff.First().envoye != "")
                return false;
            else
                return true;
            /*
            switch (value.ToString().ToLower())
            {
                case "yes":
                case "oui":
                    return true;
                case "no":
                case "non":
                    return false;
            }
            return false;*/
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "";
        }
    }
    public class CurrentTabToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (int.Parse(value.ToString()) == 2)
                return false;
            else
                return true;
            /*
            switch (value.ToString().ToLower())
            {
                case "yes":
                case "oui":
                    return true;
                case "no":
                case "non":
                    return false;
            }
            return false;*/
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "0";
                else
                    return "2";
            }
            return "0";
        }
    }

    public class CurrentItemToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == ( null))
                return false;
            else
                return true;
            /*
            switch (value.ToString().ToLower())
            {
                case "yes":
                case "oui":
                    return true;
                case "no":
                case "non":
                    return false;
            }
            return false;*/
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                if ((bool)value == true)
                    return null;
                else
                    return null;
            }
            return null;
        }
    }

    public class CurrentItemContratToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == (null))
                return false;
            else
            {
                if ((value as utilitaire.Contrat).encours == "O")
                    return true;
                else
                    return false;
            }
            /*
            switch (value.ToString().ToLower())
            {
                case "yes":
                case "oui":
                    return true;
                case "no":
                case "non":
                    return false;
            }
            return false;*/
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                if ((bool)value == true)
                    return null;
                else
                    return null;
            }
            return null;
        }
    }


    public partial class MainWindow : Window
    {
        private ObservableCollection<utilitaire.Departement> departementsel = new ObservableCollection<utilitaire.Departement>();
        //MySqlCommand mscom = new MySqlCommand();
        //MySqlCommand mscom_sel = new MySqlCommand();
        
        
        public MainWindow()
        {
            /*cities.Add(new City() {  Zip = "Mumbai", CityName = "Maharashtra", IdCity = 1 });
            cities.Add(new City() { Zip = "Pune", CityName = "Maharashtra", IdCity = 2 });
            cities.Add(new City() { Zip = "Nashik", CityName = "Maharashtra", IdCity = 3 });
            cities.Add(new City() { Zip = "Aurangabad", CityName = "Maharashtra", IdCity = 4 });*/

            /*CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
            Thread.CurrentThread.CurrentCulture = ci;*/

            /*System.Windows.FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(System.Windows.FrameworkElement),
                new System.Windows.FrameworkPropertyMetadata(
                System.Windows.Markup.XmlLanguage.GetLanguage(System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag)));
                */
            InitializeComponent();
        }


        public utilitaire UT = new utilitaire();

        public string szU, szP, szH = "";
        private Boolean bfonc = false;
        private Boolean bdepa = false;

        

        private void Window_Initialized(object sender, EventArgs e)
        {
            /*public string szU, szP, szH = "";
            string dbase = .ReadString("BDD", "databaseName");
            baseInit = dbase;*/
            
            if (UT.connecter() == false)
                return;
            //UT.InitConnection();
            UT.chargerNpaVille();
            UT.chargerLangue();
            UT.chargerNationalite();
            UT.chargerPolitesse();

            UT.chargerDepartement();
            UT.chargerFonction();
            UT.chargerInstitutions();
            UT.chargerEntreprises();
            UT.chargerBanque(cond: "", idb: "", tout: true);
            //UT.chargerSalaries();
            sfonctions = UT.fonctions;
            //(c_horairetravail as DataGridComboBoxColumn).ItemsSource = "{Temps Plein, Temps Partiel}";
            (c_politesse as DataGridComboBoxColumn).ItemsSource = UT.politesses;
            //(c_departement as DataGridComboBoxColumn).ItemsSource = UT.departements;
            //(c_fonction as DataGridComboBoxColumn).ItemsSource = UT.fonctions;
            
            (c_langue as DataGridComboBoxColumn).ItemsSource = UT.langues;
            cmb_nationalite.ItemsSource = UT.nationalites;
            Resources["shorairetravail"] = "{Temps Plein, Temps Partiel}";
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //int d = 44 / int.Parse("0");
            LoopVisualTree(p_adresse);
            LoopVisualTree(p_ent_adresse);
            LoopVisualTree(p_identite);
            cmb_langue.ItemsSource = cmb_Ent_langue.ItemsSource = cmb_inst_langue.ItemsSource = UT.langues;
            cmb_npaville.ItemsSource = cmb_Ent_npaville.ItemsSource = cmb_inst_npaville.ItemsSource = UT.cities;
            //cmb_politesse.ItemsSource = UT.politesses;
            //cmb_departement.ItemsSource = UT.departements;
            //l_departement.ItemsSource = UT.departements;
            //cmb_fonction.ItemsSource = UT.fonctions;
            //l_fonction.ItemsSource = UT.fonctions;
            //dataGrid.ItemsSource = UT.salaries;
            cmb_Entreprise.ItemsSource = UT.entreprises;
            g_param_institutions.ItemsSource = UT.institutions;
            if (g_param_institutions.HasItems)
                g_param_institutions.SelectedIndex = 0;
            cmb_institution.ItemsSource = UT.institutions;
            //g_selsalarie.DataContext = UT.selsalarie(0);
            etatbouton(bt_annuler, new RoutedEventArgs());
            //p_ent_adresse.IsEnabled = p_adresse.IsEnabled = p_identite.IsEnabled = p_param_adresseinst.IsEnabled = false;
            ed_lpp_annee.Text = DateTime.Now.Year.ToString();
            //afficheDonneesEntreprise();

            if (cmb_Entreprise.HasItems)
            {

                cmb_Entreprise.SelectedIndex = 0; // cmb_Entreprise.Items[0];
            }
            else
            {
                this.Resources["_bEntEdit"] = false;
                this.Resources["_bSalEdit"] = false;
            }
            //UT.InitConnection();

        }

        private void afficheDepartement(string ident)
        {
            IEnumerable<utilitaire.Departement> resultdep = Enumerable.Empty<utilitaire.Departement>();
            resultdep = UT.departements.Where(w => w.IdEntreprise == ident);
            //departementsel = UT.departements.Where(w => w.IdEntreprise == (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).identreprise);
            cmb_departement.ItemsSource = resultdep;
            l_departement.ItemsSource = resultdep;
            if (l_departement.HasItems)
            {
                //l_departement.SelectedIndex = -1;
                //l_departement.SelectedIndex = 0;
            }
            else
            {
                l_fonction.ItemsSource = null;
                ed_param_fonction.DataContext = null;
            }
            //ed_param_departement.IsEnabled = false; 
        }
        private void afficheDonneesEntreprise(int isel = -1)
        {
            gv_paiementnvoie.ItemsSource = null;
            
            bEtatEnt = 0;
            if (cmb_Entreprise.SelectedIndex == -1)
            {
                if (cmb_Entreprise.HasItems)
                {//bEtatEnt = 0;
                    cmb_Entreprise.SelectedIndex = 0;
                    cmb_Entreprise.SelectedItem = cmb_Entreprise.Items[0];
                }
                else
                {
                    p_ent_adresse.DataContext = null;
                    this.Resources["_bEntEdit"] = false;
                    this.Resources["_bSalEdit"] = false;

                    return;
                }

            }
            else
            {
                
                IEnumerable<utilitaire.Salarie> result = Enumerable.Empty<utilitaire.Salarie>();

                p_ent_adresse.DataContext = (cmb_Entreprise.SelectedItem as utilitaire.Entreprise);
                /*result = UT.salaries.Where(w => w.identreprise == (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).identreprise);

                
                int ii = result.Count<utilitaire.Salarie>();
                //dataGrid.DataContext= null;

                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = result;*/
                afficheDepartement((cmb_Entreprise.SelectedItem as utilitaire.Entreprise).identreprise);
                UT.chargerSalaries((cmb_Entreprise.SelectedItem as utilitaire.Entreprise).identreprise);
                if (op_paiementenvoye.IsChecked == false)
                    op_paiementenvoye.IsChecked = true;
                else
                    ChargerPaiement(1);
                gv_paiementnvoie.ItemsSource = UT.PaiementSalaireEnvoies;
                UT.chargerContrats((cmb_Entreprise.SelectedItem as utilitaire.Entreprise).identreprise);
                UT.chargerTauxEntreprises((cmb_Entreprise.SelectedItem as utilitaire.Entreprise).identreprise, DateTime.Now.Year.ToString());

                IEnumerable<IGrouping<string, string>> tauxannee = UT.TauxEntreprises.GroupBy(pet => pet.annee, pet => pet.annee);


                //var tauxannee =  UT.TauxEntreprises.GroupBy(w => w.identreprise, ann => ann.annee );
                ed_tx_annee.Text = "";
                ed_tx_annee.Text = DateTime.Now.Year.ToString();
                UT.GrillesalarieTmps.Clear();
                UT.chargerGrillesalarie(cmb_Entreprise.SelectedValue.ToString(), ed_tx_annee.Text.Trim(), 3); //Toutes les grilles

                dataGrid.ItemsSource = cmb_grille_salarie.ItemsSource = UT.salaries;
                //cmb_grille_salarie.ItemsSource = UT.salaries;

                /*if (g_contrat.HasItems)
                {
                    

                    g_contrat.SelectedIndex = 0;
                }*/
                afficheSalarie(isel);
                
            }
            
        }

        private void afficherTauxEnt(string anneesel)
        {
            gv_tauxentreprise.ItemsSource = null;
            IEnumerable<utilitaire.TauxEntreprise> res = UT.TauxEntreprises.Where(w => w.annee == anneesel);

            /*ListCollectionView gg = new ListCollectionView(UT.TauxEntreprises);
            gg.GroupDescriptions.Add(new PropertyGroupDescription("typeparam"));*/
            //IEnumerable<IGrouping<string, string>> tauxannee = res.OrderBy(w => w.rang).GroupBy(w => w.identreprise, w => w.typeparam);
            cmb_tx_inst.ItemsSource = UT.institutions;

            gv_tauxentreprise.ItemsSource = res.OrderBy(w=> w.rang);
            cmb_listecode.ItemsSource = res.OrderBy(w => w.rang);
            this.Resources["_bTauxEdit"] = false;
            this.Resources["bTauxEdit"] = false;
        }

        private void afficheSalarie(int isel = -1)
        {
            bEtat = 0;
            bEtat = 0;
            //if (currow == (sender as DataGrid).Items.IndexOf((sender as DataGrid).CurrentItem))
            //    return;
            if (dataGrid.SelectedItem == null)
            {
                if (dataGrid.HasItems == true)
                {
                    if (isel != -1)
                        dataGrid.SelectedIndex = isel;
                    else
                        dataGrid.SelectedIndex = 0;


                }
                else
                {
                    g_selsalarie.DataContext = null;
                    this.Resources["_bSalEdit"] = false;
                    bt_sal_ajout.IsEnabled = true;
                    return;
                }
            }
            /*if (cmb_fonction.ItemsSource != UT.fonctions)
                cmb_fonction.ItemsSource = UT.fonctions;
                */
            //bt_annuler_Click(bt_annuler, new RoutedEventArgs());
            g_selsalarie.DataContext = (dataGrid.SelectedItem as utilitaire.Salarie);

            //cmb_fonction.SelectedValue = (dataGrid.SelectedItem as utilitaire.Salarie).idfonction;
            /*p_adresse.IsEnabled = false;
            p_identite.IsEnabled = false;
            p_photo.IsEnabled = false;
            ed_nom.IsEnabled = false;*/
            //currow = (sender as DataGrid).Items.IndexOf((sender as DataGrid).CurrentItem);

            
            if (dataGrid.SelectedIndex > -1)
            {
                this.Resources["_bSalEdit"] = true;
                /*this.Resources["_bSalEdit"] = true;
                IEnumerable<utilitaire.Contrat>  result = UT.contrats.Where(w => w.idsalarie == (dataGrid.SelectedItem as utilitaire.Salarie).idsalaries);

                g_contrat.ItemsSource = result;*/
                UT.chargerContrats((dataGrid.SelectedItem as utilitaire.Salarie).idsalaries);
                g_contrat.ItemsSource = UT.contrats;
                p_identitedetail.DataContext = null;
                //if (g_contrat.HasItems)
                //{
                    //g_contrat.SelectedIndex = 0;
                    //this.Resources["_bContEdit"] = true;
                    affichecontrat();
                //}
                //else
                //    this.Resources["_bContEdit"] = false;
                //dataGrid.SelectedIndex = 0;

            }
            else
            {
                this.Resources["_bSalEdit"] = false;
            }
            

        }

        void LoopVisualTree(DependencyObject obj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                if (obj is TextBox)
                    ((TextBox)obj).Text = null;
                if (obj is ComboBox)
                    ((ComboBox)obj).SelectedValue = null;
                LoopVisualTree(VisualTreeHelper.GetChild(obj, i));
            }
        }
        ObservableCollection<utilitaire.Fonction> sfonctions;
        
        private void saisieZip(TextBox s, ComboBox t, int ietat)
        {
            if (s.GetType() != typeof(TextBox) || modif == true || modifEnt == true || modifInst == true)
                return;
            int kk = 0;

            bool numerik = true;
            try
            {
                kk = 2 * int.Parse(s.Text.Trim());
            }
            catch
            {
                numerik = false;
            }
            string cond = "";
            if (s.Text.Trim() == "")
            {
                t.DataContext = null;
                return;
            }
            IEnumerable<utilitaire.City> result;

            if (numerik)
                result = UT.cities.Where(w => w.Zip.IndexOf(s.Text) == 0);
            else
                result = UT.cities.Where(w => w.CityName.ToLower().Contains(s.Text.ToLower()));
            t.ItemsSource = result;

            if (ietat == 0)
            {
                if (((s as TextBox).Parent as Grid).Name == "p_adresse") //sal
                    t.SelectedValue = ((t.Parent as Grid).DataContext as utilitaire.Salarie).idville;
                if (((s as TextBox).Parent as Grid).Name == "p_ent_adresse") //ent
                    t.SelectedValue = ((t.Parent as Grid).DataContext as utilitaire.Entreprise).idville;
                if (((s as TextBox).Parent as Grid).Name == "p_param_adresseinst") //inst
                        t.SelectedValue = ((t.Parent as Grid).DataContext as utilitaire.Institution).idville;
            }
        }
        
        private void ed_npacity_TextChanged(object sender, TextChangedEventArgs e)
        {
            saisieZip(ed_npacity, cmb_npaville, bEtat);
            return;
            
            Control s = sender as Control;
            
            if (((sender as TextBox).Parent as Grid).Name == "p_adresse")
            {
                if (s.GetType() != typeof(TextBox) || modif == true || bEtat == 0)
                    return;
                
                int kk = 0;

                bool numerik = true;
                try
                {
                    kk = 2 * int.Parse(ed_npacity.Text.Trim());
                }
                catch
                {
                    numerik = false;
                }
                string cond = "";
                if (ed_npacity.Text.Trim() == "")
                {
                    cmb_npaville.ItemsSource = null;
                    return;
                }
                IEnumerable<utilitaire.City> result;

                if (numerik)
                    result = UT.cities.Where(w => w.Zip.IndexOf(ed_npacity.Text) == 0);
                else
                    result = UT.cities.Where(w => w.CityName.ToLower().Contains(ed_npacity.Text.ToLower()));
                cmb_npaville.ItemsSource = result;

                if (bEtat == 0)
                    cmb_npaville.SelectedValue = (dataGrid.SelectedItem as utilitaire.Salarie).idville;
            }
            else if (((sender as TextBox).Parent as Grid).Name == "p_ent_adresse")
            {
                if (s.GetType() != typeof(TextBox) || modifEnt == true || bEtatEnt == 0)
                    return;

                int kk = 0;

                bool numerik = true;
                try
                {
                    kk = 2 * int.Parse(ed_Ent_npacity.Text.Trim());
                }
                catch
                {
                    numerik = false;
                }
                string cond = "";
                if (ed_Ent_npacity.Text.Trim() == "")
                {
                    cmb_Ent_npaville.ItemsSource = null;
                    return;
                }
                IEnumerable<utilitaire.City> result;

                if (numerik)
                    result = UT.cities.Where(w => w.Zip.IndexOf(ed_Ent_npacity.Text) == 0);
                else
                    result = UT.cities.Where(w => (w.CityName.ToLower().Contains(ed_Ent_npacity.Text)));
                cmb_Ent_npaville.ItemsSource = result;

                if (bEtatEnt == 0)
                    cmb_Ent_npaville.SelectedValue = (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).idville;
            }
            else if (((sender as TextBox).Parent as Grid).Name == "p_param_adresseinst")
            {
                if (s.GetType() != typeof(TextBox) || modifInst == true || bEtatInst == 0)
                    return;

                int kk = 0;

                bool numerik = true;
                try
                {
                    kk = 2 * int.Parse(ed_inst_npacity.Text.Trim());
                }
                catch
                {
                    numerik = false;
                }
                string cond = "";
                if (ed_inst_npacity.Text.Trim() == "")
                {
                    cmb_inst_npaville.ItemsSource = null;
                    return;
                }
                IEnumerable<utilitaire.City> result;

                if (numerik)
                    result = UT.cities.Where(w => w.Zip.IndexOf(ed_inst_npacity.Text) == 0);
                else
                    result = UT.cities.Where(w => (w.CityName.ToLower().Contains(ed_inst_npacity.Text)));
                cmb_inst_npaville.ItemsSource = result;

                if (bEtatInst == 0)
                    cmb_inst_npaville.SelectedValue = (g_param_institutions.SelectedItem as utilitaire.Institution).idville;
            }
            
        }

        private void cmb_langue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_langue.SelectedIndex == -1)
                return;
            IEnumerable<utilitaire.Politesse> result;
            

            result = UT.politesses.Where(w => w.IdLangue == (cmb_langue.SelectedItem as utilitaire.Language).IdLanguage);

            cmb_politesse.ItemsSource = result;
            if (bEtat == 0) //
            {
                cmb_politesse.SelectedValue = (dataGrid.SelectedItem as utilitaire.Salarie).idpolitesse;
            }
        }

        private void cmb_departement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_departement.SelectedIndex == -1)
                return;
            if (bEtatCont > 0 && g_selsalarie.DataContext != null)
            {
                (p_identitedetail.DataContext as utilitaire.Contrat).departementnom = (cmb_departement.SelectedItem as utilitaire.Departement).DepartementNom;
            }
            chargerfonction();
            //}
        }

        private void chargerfonction()
        {
            IEnumerable<utilitaire.Fonction> result;
            result = null;

            result = UT.fonctions.Where(w => w.IdDepartement == (cmb_departement.SelectedItem as utilitaire.Departement).IdDepartement);

            cmb_fonction.ItemsSource = result;
            if (bEtat == 0)
                cmb_fonction.SelectedValue = (p_identitedetail.DataContext as utilitaire.Contrat).idfonction;
        }

        IEnumerable<utilitaire.Salarie> Curr_Salarie;
        public int _bEtatEnt = 0;
        public int _bEtatInst = 0;
        int currow = -1;

        public int _bEtat = 0;
        public int bEtat = 0;
        /*
        public int bEtat
        {
            get { return _bEtat; }
            set
            {
                _bEtat = value;
                if (_bEtat == 0)
                {
                    p_adresse.IsEnabled = false;
                    p_identite.IsEnabled = false;
                    p_photo.IsEnabled = false;
                    IEnumerable<utilitaire.City> result;

                    
                        result = UT.cities.Where(w => w.Zip.IndexOf(ed_npacity.Text) == 0);
                    cmb_npaville.ItemsSource = result;// UT.cities;
                }
                else
                {
                    p_adresse.IsEnabled = true;
                    p_identite.IsEnabled = true;
                    p_photo.IsEnabled = true;
                    int kk = 0;

                    bool numerik = true;
                    try
                    {
                        kk = 2 * int.Parse(ed_npacity.Text.Trim());
                    }
                    catch
                    {
                        numerik = false;
                    }
                    string cond = "";
                    if (ed_npacity.Text.Trim() == "")
                    {
                        cmb_npaville.ItemsSource = null;
                        return;
                    }
                    IEnumerable<utilitaire.City> result;

                    if (numerik)
                        result = UT.cities.Where(w => w.Zip.IndexOf(ed_npacity.Text) == 0);
                    else
                        result = UT.cities.Where(w => w.CityName.ToLower().Contains(ed_npacity.Text.ToLower()));
                    //cmb_inst_npaville.ItemsSource = null;
                    cmb_npaville.ItemsSource = result;
                }
            }
        }*/
        public int bEtatEnt = 0;
        /*
        public int bEtatEnt
        {
            get { return _bEtatEnt; }
            set
            {
                _bEtatEnt = value;
                if (_bEtatEnt == 0)
                {
                    //p_ent_adresse.IsEnabled = false;
                    //cmb_Entreprise.IsEnabled = true;
                    IEnumerable<utilitaire.City> result;
                    result = UT.cities.Where(w => w.Zip.IndexOf(ed_Ent_npacity.Text) == 0);
                    cmb_Ent_npaville.ItemsSource = result;// UT.cities;
                }
                else
                {
                    //p_ent_adresse.IsEnabled = true;
                    //cmb_Entreprise.IsEnabled = false;
                    
                    int kk = 0;

                    bool numerik = true;
                    try
                    {
                        kk = 2 * int.Parse(ed_Ent_npacity.Text.Trim());
                    }
                    catch
                    {
                        numerik = false;
                    }
                    string cond = "";
                    if (ed_Ent_npacity.Text.Trim() == "")
                    {
                        cmb_Ent_npaville.ItemsSource = null;
                        return;
                    }
                    IEnumerable<utilitaire.City> result;

                    if (numerik)
                        result = UT.cities.Where(w => w.Zip.IndexOf(ed_Ent_npacity.Text) == 0);
                    else
                        result = UT.cities.Where(w => w.CityName.ToLower().Contains(ed_Ent_npacity.Text.ToLower()));
                    //cmb_inst_npaville.ItemsSource = null;
                    cmb_Ent_npaville.ItemsSource = result;
                }
            }
        }*/
        public int bEtatInst = 0;
        public int bEtatCont = 0;
        public int bEtatTaux = 0;
        /*
        public int bEtatInst
        {
            get { return _bEtatInst; }
            set
            {
                _bEtatInst = value;
                if (_bEtatInst == 0)
                {
                    //p_param_adresseinst.IsEnabled = false;
                    
                    
                    //cmb_inst_npaville.ItemsSource = UT.cities;
                    IEnumerable<utilitaire.City> result;
                    result = UT.cities.Where(w => w.Zip.IndexOf(ed_inst_npacity.Text) == 0);
                    cmb_inst_npaville.ItemsSource = result;// UT.cities;
                    //cmb_Entreprise.IsEnabled = true;
                }
                else
                {
                    //p_param_adresseinst.IsEnabled = true;
                    int kk = 0;

                    bool numerik = true;
                    try
                    {
                        kk = 2 * int.Parse(ed_inst_npacity.Text.Trim());
                    }
                    catch
                    {
                        numerik = false;
                    }
                    string cond = "";
                    if (ed_inst_npacity.Text.Trim() == "")
                    {
                        cmb_inst_npaville.ItemsSource = null;
                        return;
                    }
                    IEnumerable<utilitaire.City> result;

                    if (numerik)
                        result = UT.cities.Where(w => w.Zip.IndexOf(ed_inst_npacity.Text) == 0);
                    else
                        result = UT.cities.Where(w => w.CityName.ToLower().Contains(ed_inst_npacity.Text.ToLower()));
                        //cmb_inst_npaville.ItemsSource = null;
                    cmb_inst_npaville.ItemsSource = result;
                    //cmb_Entreprise.IsEnabled = false;
                }
            }
        }*/

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem == null) // || dataGrid.SelectedItem == dataGrid.CurrentItem)
            {
                this.Resources["_bSalEdit"] = false;
                return;
            }
            afficheSalarie();
            //gv_donneebasesalarie.ItemsSource = null;
            chargerDonneebase();
            UT.chargerDonneeSalarieResume((dataGrid.SelectedItem as utilitaire.Salarie).idsalaries);
            gv_donneeresume.ItemsSource = UT.DonneeSalarieResumes;

        }

        private void bt_ajout_Click(object sender, RoutedEventArgs e)
        {
            if (tb_tableau.SelectedIndex == 1) //salariés
            {
                bEtat = 1;
                utilitaire.Salarie newitem = new utilitaire.Salarie();
                newitem.identreprise = (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).identreprise;
                g_selsalarie.DataContext = newitem;
                //LoopVisualTree(g_selsalarie);
            }
            else if (tb_tableau.SelectedIndex == 0) //entreprise
            {
                
            }
            else if (tb_tableau.SelectedIndex == 2) //institution
            {
                
            }
            etatbouton(sender, e);
        }

        public void bouton_click()
        {

        }

        public void miseajour(System.Windows.Controls.Grid cont)
        {

            foreach (object c in (cont).Children)
            {
                
                if (c.GetType() == typeof(System.Windows.Controls.TextBox))
                {
                    System.Windows.Controls.TextBox ff = c as System.Windows.Controls.TextBox;

                    BindingExpression b = ff.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty);
                    if (b == null)
                        continue;
                    b.UpdateSource();
                }
                else if (c.GetType() == typeof(System.Windows.Controls.DatePicker))
                {
                    System.Windows.Controls.DatePicker ff = c as System.Windows.Controls.DatePicker;

                    BindingExpression b = ff.GetBindingExpression(System.Windows.Controls.DatePicker.SelectedDateProperty);
                    if (b == null)
                        continue;
                    ff.SelectedDate = DateTime.Parse(string.Format("{0:dd/MM/yyyy}", ff.SelectedDate));
                    b.UpdateSource();
                }
                if (c.GetType() == typeof(System.Windows.Controls.ComboBox))
                {
                    System.Windows.Controls.ComboBox ff = c as System.Windows.Controls.ComboBox;

                    try
                    {
                        BindingExpression b = ff.GetBindingExpression(System.Windows.Controls.ComboBox.SelectedValueProperty);
                        if (ff.Name.ToLower().Contains("npaville") && cont.Name == "p_param_adresseinst")
                        {
                            if (bEtatInst == 2) //modif
                            {
                                (g_param_institutions.SelectedItem as utilitaire.Institution).ville = (cmb_inst_npaville.SelectedItem as utilitaire.City).CityName;
                            }
                            else if (bEtatInst == 1) //ajout
                                (p_param_adresseinst.DataContext as utilitaire.Institution).ville = (cmb_inst_npaville.SelectedItem as utilitaire.City).CityName;

                            //BindingExpression bb = g_param_institutions.GetBindingExpression(System.Windows.Controls.DataGrid.SelectedValuePathProperty);
                            //bb.UpdateSource();
                        }
                        else if (ff.Name.ToLower().Contains("npaville") && cont.Name == "p_ent_adresse")
                        {
                            if (bEtatEnt == 2)
                                (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).ville = (cmb_Ent_npaville.SelectedItem as utilitaire.City).CityName;
                            

                            ///cmb_Entreprise.GetBindingExpression(System.Windows.Controls.ComboBox.ItemsSourceProperty).Update();
                            //BindingExpression bb = g_param_institutions.GetBindingExpression(System.Windows.Controls.DataGrid.SelectedValuePathProperty);
                            //bb.UpdateSource();
                        }
                        else if (ff.Name.ToLower().Contains("npaville") && cont.Name == "p_adresse")
                        {
                            if (bEtat == 2)
                                (p_adresse.DataContext as utilitaire.Salarie).idville = (cmb_npaville.SelectedItem as utilitaire.City).IdVille;
                            

                            ///cmb_Entreprise.GetBindingExpression(System.Windows.Controls.ComboBox.ItemsSourceProperty).Update();
                            //BindingExpression bb = g_param_institutions.GetBindingExpression(System.Windows.Controls.DataGrid.SelectedValuePathProperty);
                            //bb.UpdateSource();
                        }
                        else if (ff.Name.ToLower().Contains("banque") && cont.Name == "p_adresse")
                        {
                            if (cmb_banque.SelectedItem == null)
                                (p_adresse.DataContext as utilitaire.Salarie).idbanque = "";
                            else
                                (p_adresse.DataContext as utilitaire.Salarie).idbanque = (cmb_banque.SelectedItem as utilitaire.Banque).idbanque;


                            ///cmb_Entreprise.GetBindingExpression(System.Windows.Controls.ComboBox.ItemsSourceProperty).Update();
                            //BindingExpression bb = g_param_institutions.GetBindingExpression(System.Windows.Controls.DataGrid.SelectedValuePathProperty);
                            //bb.UpdateSource();
                        }
                        else if (ff.Name.ToLower().Contains("banque") && cont.Name == "p_ent_adresse")
                        {
                            if (cmb_Ent_banque.SelectedItem == null)
                                (p_ent_adresse.DataContext as utilitaire.Entreprise).idbanque = "";
                            else
                                (p_ent_adresse.DataContext as utilitaire.Entreprise).idbanque = (cmb_Ent_banque.SelectedItem as utilitaire.Banque).idbanque;


                            ///cmb_Entreprise.GetBindingExpression(System.Windows.Controls.ComboBox.ItemsSourceProperty).Update();
                            //BindingExpression bb = g_param_institutions.GetBindingExpression(System.Windows.Controls.DataGrid.SelectedValuePathProperty);
                            //bb.UpdateSource();
                        }
                        else if (ff.Name.ToLower().Contains("cmb_tx_") && cont.Name == "p_tauxentreprise")
                        {
                            if (bEtatTaux == 2 && cmb_tx_inst.SelectedItem != null)
                            {
                                (p_tauxentreprise.DataContext as utilitaire.TauxEntreprise).idinstitution = (cmb_tx_inst.SelectedItem as utilitaire.Institution).idinstitution;
                                (p_tauxentreprise.DataContext as utilitaire.TauxEntreprise).instnom = (cmb_tx_inst.SelectedItem as utilitaire.Institution).societe;
                            }
                            if (cmb_tx_inst.SelectedItem == null)
                            {
                                (p_tauxentreprise.DataContext as utilitaire.TauxEntreprise).idinstitution = "";
                                (p_tauxentreprise.DataContext as utilitaire.TauxEntreprise).instnom = "";
                            }

                            ///cmb_Entreprise.GetBindingExpression(System.Windows.Controls.ComboBox.ItemsSourceProperty).Update();
                            //BindingExpression bb = g_param_institutions.GetBindingExpression(System.Windows.Controls.DataGrid.SelectedValuePathProperty);
                            //bb.UpdateSource();
                        }

                        /*if (b == null)
                            continue;*/
                        b.UpdateSource();

                    }
                    catch
                    {
                        BindingExpression b = ff.GetBindingExpression(System.Windows.Controls.ComboBox.SelectedItemProperty);

                        /*if (b == null)
                            continue;*/
                        b.UpdateSource();
                    }

                }
            }
        }
        private void bt_valider_Click(object sender, RoutedEventArgs e)
        {
            if (tb_tableau.SelectedIndex == 1) //salariés
            {
                if (bEtat == 1)
                {
                    miseajour(p_adresse);
                    miseajour(p_identite);
                    if (UT.enregistrerSalarie(g_selsalarie.DataContext as utilitaire.Salarie, 1) == true)
                    {
                        System.Windows.Forms.MessageBox.Show("Ajout éffectué avec succès !", "Ajout d'employé", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                        UT.salaries.Add(g_selsalarie.DataContext as utilitaire.Salarie);
                    }
                    else
                        System.Windows.Forms.MessageBox.Show("Il y a eu une erreur !", "Ajout d'employé", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

                }
                else if (bEtat == 2)
                {
                    miseajour(p_adresse);
                    miseajour(p_identite);
                    if (UT.enregistrerSalarie(g_selsalarie.DataContext as utilitaire.Salarie, 2) == true)
                    {
                        System.Windows.Forms.MessageBox.Show("Modification éffectuée avec succès !", "Modification d'employé", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                        //UT.salaries.Add(g_selsalarie.DataContext as utilitaire.Salarie);
                    }
                    else
                        System.Windows.Forms.MessageBox.Show("Il y a eu une erreur !", "Modification d'employé", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

                }
                bEtat = 0;
            }
            else if (tb_tableau.SelectedIndex == 0) //entreprise
            {
                
            }
            else if (tb_tableau.SelectedIndex == 2) //institution
            {
                
            }
            etatbouton(sender, e);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
                
        }

        private void bt_modifier_Click(object sender, RoutedEventArgs e)
        {
            if (tb_tableau.SelectedIndex == 1) //salariés
            {
                bEtat = 2;
                //p_identite.IsEnabled = p_adresse.IsEnabled = p_photo.IsEnabled = true;
                chargerfonction();
                /*UT.selsalarie((g_selsalarie.DataContext as utilitaire.Salarie).idsalaries);
                g_selsalarie.DataContext = null;
                g_selsalarie.DataContext = UT.salaries_sel.First<utilitaire.Salarie>();*/


            }
            else if (tb_tableau.SelectedIndex == 0) //entreprise
            {
                p_ent_adresse.IsEnabled = p_photo.IsEnabled = true;
            }
            else if (tb_tableau.SelectedIndex == 2) //institutions
            {
                p_param_adresseinst.IsEnabled = true;
                bEtatInst = 2;
            }
            etatbouton(sender, e);
        }

        private void etatbouton(object sender, RoutedEventArgs e)
        {
            if ((sender as Button) == bt_modifier || (sender as Button) == bt_ajout) 
            {
                bt_ajout.IsEnabled = bt_modifier.IsEnabled = false;
                bt_valider.IsEnabled = bt_annuler.IsEnabled = true;
            }
            else
            {
                bt_ajout.IsEnabled = bt_modifier.IsEnabled = true;
                bt_valider.IsEnabled = bt_annuler.IsEnabled = false;
            }
        }
        private void cmb_fonction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_fonction.SelectedItem == null)
                return;
            if (bEtatCont > 0 && g_selsalarie.DataContext != null)
            {
                
                (p_identitedetail.DataContext as utilitaire.Contrat).fonctionnom = (cmb_fonction.SelectedItem as utilitaire.Fonction).FonctionNom;
            }
        }

        private void image_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void bt_photo_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.InitialDirectory = Environment.CurrentDirectory + @"\Images\";
            dlg.Filter = "Image JPG (*.jpg)|*.jpg|Image PNG|*.png";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFileName = dlg.SafeFileName;
                if (!dlg.FileName.Contains(Environment.CurrentDirectory + @"\Images\"))
                    System.IO.File.Copy(dlg.FileName, Environment.CurrentDirectory + @"\Images\" + dlg.SafeFileName);
                //im_image.Source.SetValue("UriSource", selectedFileName);
                (dataGrid.SelectedItem as utilitaire.Salarie).photo = selectedFileName;
                (g_selsalarie.DataContext as utilitaire.Salarie).photo = selectedFileName;
                lb_photo.Text = selectedFileName;

            }
        }

    

        private void ed_nom_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            
        }

        private void lb_photo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lb_photo.Text == "")
            {
                im_image.Source = null;
                return;
            }
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(Environment.CurrentDirectory + @"\Images\" + @lb_photo.Text.ToString());
            bitmap.EndInit();
            im_image.Source = bitmap;
        }


        private void cmb_Entreprise_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (bEtatEnt > 0)
                return;
            if (cmb_Entreprise.HasItems == true && cmb_Entreprise.SelectedValue != null && cmb_Entreprise.SelectedIndex == -1)
            {
                cmb_Entreprise.SelectedItem = p_ent_adresse.DataContext;
                return;
            }
            afficheDonneesEntreprise();
        }

        bool modif = false;

        private void dataGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            string d = "";
        }

        private void dataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            
        }

        private void afficheinstitutions()
        {
            this.Resources["bInsEdit"] = false;

            if (g_param_institutions.SelectedItem == null) // || dataGrid.SelectedItem == dataGrid.CurrentItem)
                return;
            p_param_adresseinst.IsEnabled = true;
            bEtatInst = 0;
            p_param_adresseinst.DataContext = null;
            p_param_adresseinst.DataContext = g_param_institutions.SelectedItem as utilitaire.Institution;
        }

        private void g_param_institutions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (g_param_institutions.SelectedItem == null) // || dataGrid.SelectedItem == dataGrid.CurrentItem)
                return;
            bEtatInst = 0;
            this.Resources["bInsEdit"] = false;
            this.Resources["_bEdit"] = true;
            p_param_adresseinst.DataContext = (g_param_institutions.SelectedItem as utilitaire.Institution);
            //if (currow == (sender as DataGrid).Items.IndexOf((sender as DataGrid).CurrentItem))
            //    return;

            //bt_annuler_Click(bt_annuler, new RoutedEventArgs());
            //p_param_adresseinst.DataContext = (g_param_institutions.SelectedItem as utilitaire.Institution);


            //cmb_fonction.SelectedValue = (dataGrid.SelectedItem as utilitaire.Salarie).idfonction;
            /*p_adresse.IsEnabled = false;
            p_identite.IsEnabled = false;
            p_photo.IsEnabled = false;
            ed_nom.IsEnabled = false;*/

        }

        private void ed_Ent_npacity_TextChanged(object sender, TextChangedEventArgs e)
        {
            saisieZip(ed_Ent_npacity, cmb_Ent_npaville, bEtatEnt);
        }

        private void ed_inst_npacity_TextChanged(object sender, TextChangedEventArgs e)
        {
            saisieZip(ed_inst_npacity, cmb_inst_npaville, bEtatInst);
        }

        bool modifEnt = false;

        private void bt_annuler_Click(object sender, RoutedEventArgs e)
        {
            if (tb_tableau.SelectedIndex == 1) //salariés
            {
                bEtat = 0;
                //p_identite.IsEnabled = p_adresse.IsEnabled = p_photo.IsEnabled = true;

                g_selsalarie.DataContext = null;
                g_selsalarie.DataContext = dataGrid.SelectedItem as utilitaire.Salarie;

            }
            else if (tb_tableau.SelectedIndex == 0) //entreprise
            {
                

            }
            else if (tb_tableau.SelectedIndex == 2) //institutions
            {
                
            }
            etatbouton(sender, e);


        }

        private void ed_inst_npacity_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            saisieZip(ed_inst_npacity, cmb_inst_npaville, bEtatInst);
            return;
        }

        private void cmb_Ent_npaville_TargetUpdated(object sender, DataTransferEventArgs e)
        {

        }

        int bEtatDepa = 0;
        private void l_departement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //bt_depa_suppr.IsEnabled = ed_param_departement.IsEnabled = false;
            l_fonction.SelectedIndex = -1;
            l_fonction.ItemsSource = null;

            if (l_departement.SelectedIndex == -1)
                return;
            ed_param_fonction.DataContext = null;
            //ed_param_departement.Text = ed_param_fonction.Text = "";
            affichefonction((l_departement.SelectedItem as utilitaire.Departement).IdDepartement);

            if (l_departement.SelectedIndex != -1)
            {
                ed_param_departement.DataContext = l_departement.SelectedItem as utilitaire.Departement;
                //bt_depa_suppr.IsEnabled = ed_param_departement.IsEnabled = true;
            }
            //ed_param_fonction.DataContext = l_fonction.SelectedItem as utilitaire.Fonction;
            //bt_fonc_suppr.IsEnabled = false;
            /*if (l_fonction.HasItems)
            {
                l_fonction.SelectedIndex = -1;
            }*/
            bEtatDepa = 0;
        }

        private void affichefonction(string iddepartement)
        {
            ed_param_fonction.Text = "";
            IEnumerable<utilitaire.Fonction> result;
            result = UT.fonctions.Where(w => w.IdDepartement == iddepartement);
            l_fonction.ItemsSource = result;
            //ed_param_fonction.IsEnabled = false;
        }

        private void l_fonction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*if (l_fonction.SelectedItem != null)
                bt_fonc_suppr.IsEnabled = ed_param_fonction.IsEnabled = true;
            else
                bt_fonc_suppr.IsEnabled = ed_param_fonction.IsEnabled = false;*/
            ed_param_fonction.DataContext = l_fonction.SelectedItem as utilitaire.Fonction;
            bEtatFonc = 0;

        }

        private void bt_depa_ajout_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_Entreprise.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez d'abord sélectionner un entreprise ou en ajouter !", "Ajout Département", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            l_departement.SelectedItem = null;
            l_departement.SelectedIndex = -1;
            utilitaire.Departement newdep = new utilitaire.Departement();
            ed_param_departement.DataContext = newdep;
            bEtatDepa = 1;
            ed_param_departement.IsEnabled = true;
            ed_param_departement.Focus();
        }
        private void bt_fonc_ajout_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_Entreprise.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez d'abord sélectionner un entreprise ou en ajouter !", "Ajout Fonction", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            l_fonction.SelectedItem = null;
            l_fonction.SelectedIndex = -1;
            utilitaire.Fonction newdep = new utilitaire.Fonction();
            ed_param_fonction.DataContext = newdep;
            bEtatFonc = 1;
            //ed_param_fonction.IsEnabled = true;
            ed_param_fonction.Focus();
        }

        int bEtatFonc = 0;
        private void ed_param_departement_KeyUp(object sender, KeyEventArgs e)
        {
            if (ed_param_departement.Text.Trim() == "" || ed_param_departement.DataContext == null)
                return;
            if (e.Key == Key.Enter)
            {
                (ed_param_departement.DataContext as utilitaire.Departement).IdEntreprise = (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).identreprise;
                BindingExpression b = ed_param_departement.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty);
                b.UpdateSource();
                if (bEtatDepa == 1) //ajout
                {

                    int newid = UT.enregistrerDepartement(ed_param_departement.DataContext as utilitaire.Departement, 1);
                    (ed_param_departement.DataContext as utilitaire.Departement).IdDepartement = newid.ToString();
                    UT.departements.Add(ed_param_departement.DataContext as utilitaire.Departement);

                    afficheDepartement((cmb_Entreprise.SelectedItem as utilitaire.Entreprise).identreprise);
                    l_departement.SelectedItem = null;
                    l_departement.SelectedItem = ed_param_departement.DataContext as utilitaire.Departement;
                }
                else
                {
                    UT.enregistrerDepartement(ed_param_departement.DataContext as utilitaire.Departement, 2);

                }
            }
        }

        private void bt_depa_suppr_Click(object sender, RoutedEventArgs e)
        {
            
            System.Windows.Forms.DialogResult rep = System.Windows.Forms.MessageBox.Show("Etes-vous sûre de supprimer ce département, les fonctions s'y référant seront aussi supprimées ?", "Paramétrage", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (rep == System.Windows.Forms.DialogResult.Yes)
            {
                UT.enregistrerDepartement(l_departement.SelectedItem as utilitaire.Departement, 3);
                UT.enregistrerFonction(null, 3, (l_departement.SelectedItem as utilitaire.Departement).IdDepartement);
                UT.departements.Remove(l_departement.SelectedItem as utilitaire.Departement);
                afficheDepartement((cmb_Entreprise.SelectedItem as utilitaire.Entreprise).identreprise);
                ed_param_departement.DataContext = null;
                //affichefonction((l_departement.SelectedItem as utilitaire.Departement).IdDepartement);
            }
        }
        private void bt_fonc_suppr_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult rep = System.Windows.Forms.MessageBox.Show("Etes-vous sûre de supprimer cette fonction ?", "Paramétrage", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (rep == System.Windows.Forms.DialogResult.Yes)
            {
                UT.enregistrerFonction(l_fonction.SelectedItem as utilitaire.Fonction, 3); 
                UT.fonctions.Remove(l_fonction.SelectedItem as utilitaire.Fonction);
                    affichefonction((l_departement.SelectedItem as utilitaire.Departement).IdDepartement);
                ed_param_fonction.DataContext = null;
            }
        }
        private void ed_param_fonction_KeyUp(object sender, KeyEventArgs e)
        {
            if(ed_param_fonction.Text.Trim() == "" || ed_param_fonction.DataContext == null)
                return;
            if (e.Key == Key.Enter)
            {
                (ed_param_fonction.DataContext as utilitaire.Fonction).IdDepartement = (l_departement.SelectedItem as utilitaire.Departement).IdDepartement;
                BindingExpression b = ed_param_fonction.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty);
                b.UpdateSource();
                if (bEtatFonc == 1) //ajout
                {

                    int newid = UT.enregistrerFonction(ed_param_fonction.DataContext as utilitaire.Fonction, typeMaj: 1);
                    (ed_param_fonction.DataContext as utilitaire.Fonction).IdFonction = newid.ToString();
                    UT.fonctions.Add(ed_param_fonction.DataContext as utilitaire.Fonction);

                    affichefonction((l_departement.SelectedItem as utilitaire.Departement).IdDepartement);

                    l_fonction.SelectedValue = ed_param_fonction.DataContext as utilitaire.Fonction;
                }
                else
                {
                    UT.enregistrerFonction(l_fonction.SelectedItem as utilitaire.Fonction, typeMaj: 2);

                }
            }
        }

        private void bt_inst_suppr_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult rep = System.Windows.Forms.MessageBox.Show("Etes-vous sûre de supprimer cette Institutions Sociales ?", "Gestion Inst. Sociales", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (rep == System.Windows.Forms.DialogResult.Yes)
            {
                UT.enregistrerInstitution(p_param_adresseinst.DataContext as utilitaire.Institution, 3);
                UT.institutions.Remove(p_param_adresseinst.DataContext as utilitaire.Institution);
                //afficheDonneesEntreprise();
                g_param_institutions.ItemsSource = null;
                g_param_institutions.ItemsSource = UT.institutions;
                p_param_adresseinst.DataContext = null;
                if (g_param_institutions.HasItems)
                    g_param_institutions.SelectedIndex = 0; 

            }

        }

        private void bt_inst_ajout_Click(object sender, RoutedEventArgs e)
        {
            bEtatInst = 1;
            this.Resources["bInsEdit"] = true;
            utilitaire.Institution newitem = new utilitaire.Institution();
            p_param_adresseinst.DataContext = newitem;
        }

        private void bt_inst_valid_Click(object sender, RoutedEventArgs e)
        {
            if (testerchamp(p_param_adresseinst))
            {

                return;
            }
            
            if (bEtatInst == 1)
            {
                miseajour(p_param_adresseinst);
                if (UT.enregistrerInstitution(p_param_adresseinst.DataContext as utilitaire.Institution, 1) == true)
                {
                    System.Windows.Forms.MessageBox.Show("Ajout éffectué avec succès !", "Ajout d'institution", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    UT.institutions.Add(p_param_adresseinst.DataContext as utilitaire.Institution);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Il y a eu une erreur !", "Ajout d'institution", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    //this.Resources["bInsEdit"] = false;
                    return;
                }
            }
            else if (bEtatInst == 2)
            {
                miseajour(p_param_adresseinst);
                if (UT.enregistrerInstitution(p_param_adresseinst.DataContext as utilitaire.Institution, 2) == true)
                {
                    System.Windows.Forms.MessageBox.Show("Modification éffectuée avec succès !", "Modification d'institution", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    //UT.salaries.Add(g!v_selsalarie.DataContext as utilitaire.Salarie);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Il y a eu une erreur !", "Modification d'institution", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return;
                }
            }
            this.Resources["bInsEdit"] = false;
            int sel = -1;
            if (bEtatInst == 1)
            {
                UT.chargerInstitutions();
                g_param_institutions.ItemsSource = null;
                g_param_institutions.ItemsSource = UT.institutions;
                g_param_institutions.SelectedItem = p_param_adresseinst.DataContext as utilitaire.Institution;
            }
            else
            {
                sel = g_param_institutions.SelectedIndex;
                g_param_institutions.ItemsSource = null;
                g_param_institutions.ItemsSource = UT.institutions;
                g_param_institutions.SelectedIndex = sel;
            }
            bEtatInst = 0;
        }

        private bool testerchamp(Grid cont)
        {
            bool ret = false;

            foreach(object c in cont.Children)
            {
                if (c.GetType().Name == "TextBox" || c.GetType().Name == "ComboBox" || c.GetType().Name == "DatePicker")
                {
                    
                    Color bb = ((c as Control).BorderBrush as SolidColorBrush).Color;
                    //if (bb.ToString() == "#FFABADB3")
                    if (bb == Colors.Red)
                    {
                        ret = true;
                        MessageBox.Show("Il y a un ou plusieurs champs requits qui ne sont pas remplis ");

                        return ret;
                    }
                }
            }
            return ret;
        }

        private void bt_inst_modif_Click(object sender, RoutedEventArgs e)
        {
            bEtatInst = 2;
            this.Resources["bInsEdit"] = true;
        }

        private void bt_inst_annul_Click(object sender, RoutedEventArgs e)
        {
            this.Resources["bInsEdit"] = false;
            p_param_adresseinst.DataContext = null;
            afficheinstitutions();
        }

        private void bt_ent_ajout_Click(object sender, RoutedEventArgs e)
        {
            
            bEtatEnt = 1;
            this.Resources["bEntEdit"] = true;
            this.Resources["_bEntEdit"] = false;
            cmb_Ent_npaville.ItemsSource = null;
            //cmb_Entreprise.ItemsSource = null;
            //cmb_Entreprise.SelectedIndex = -1;
            utilitaire.Entreprise newitem = new utilitaire.Entreprise();
            p_ent_adresse.DataContext = newitem;
        }

        private void bt_ent_valider_Click(object sender, RoutedEventArgs e)
        {
            if (testerchamp(p_ent_adresse))
            {

                return;
            }
            if (bEtatEnt == 1)
            {
                miseajour(p_ent_adresse);
                if (UT.enregistrerEntreprise(p_ent_adresse.DataContext as utilitaire.Entreprise, 1) == true)
                {
                    System.Windows.Forms.MessageBox.Show("Ajout éffectué avec succès !", "Ajout d'entreprise", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    UT.entreprises.Add(p_ent_adresse.DataContext as utilitaire.Entreprise);
                    cmb_Entreprise.ItemsSource = null;

                    cmb_Entreprise.ItemsSource = UT.entreprises;
                    cmb_Entreprise.SelectedItem = p_ent_adresse.DataContext as utilitaire.Entreprise;
                }
                else
                    System.Windows.Forms.MessageBox.Show("Il y a eu une erreur !", "Ajout d'entreprise", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

            }
            else if (bEtatEnt == 2)
            {
                miseajour(p_ent_adresse);
                if (UT.enregistrerEntreprise(p_ent_adresse.DataContext as utilitaire.Entreprise, 2) == true)
                {
                    System.Windows.Forms.MessageBox.Show("Modification éffectuée avec succès !", "Modification d'entreprise", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    //UT.salaries.Add(g!v_selsalarie.DataContext as utilitaire.Salarie);
                    int sel = cmb_Entreprise.SelectedIndex;
                    cmb_Entreprise.ItemsSource = null;

                    cmb_Entreprise.ItemsSource = UT.entreprises;
                    if (sel == -1)
                        sel = 0;
                    cmb_Entreprise.SelectedIndex = sel;
                }
                else
                    System.Windows.Forms.MessageBox.Show("Il y a eu une erreur !", "Modification d'entreprise", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

            }
            bEtatEnt = 0;
            this.Resources["_bEntEdit"] = true;
            this.Resources["bEntEdit"] = false;
        }

        private void bt_ent_annuler_Click(object sender, RoutedEventArgs e)
        {
            bEtatEnt = 0;
            this.Resources["bEntEdit"] = false;
            p_ent_adresse.DataContext = null;
            p_ent_adresse.DataContext = (cmb_Entreprise.SelectedItem as utilitaire.Entreprise);
            if (cmb_Entreprise.SelectedIndex == -1)
                this.Resources["_bEntEdit"] = false;
            else
                this.Resources["_bEntEdit"] = true;

        }

        private void bt_ent_modifier_Click(object sender, RoutedEventArgs e)
        {
            bEtatEnt = 2;
            this.Resources["_bEntEdit"] = false;
            this.Resources["bEntEdit"] = true;
        }

        private void bt_ent_suppr_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult rep = System.Windows.Forms.MessageBox.Show("Etes-vous sûre de supprimer cet Entreprise ?", "Entreprise", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (rep == System.Windows.Forms.DialogResult.Yes)
            {
                UT.enregistrerEntreprise(cmb_Entreprise.SelectedItem as utilitaire.Entreprise, 3);
                UT.entreprises.Remove(cmb_Entreprise.SelectedItem as utilitaire.Entreprise);
                //affichefonction((l_departement.SelectedItem as utilitaire.Departement).IdDepartement);
            }
        }

        private void bt_sal_ajout_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_Entreprise.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez d'abord sélectionner un entreprise ou en ajouter !", "Ajout Salarié", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (cmb_departement.HasItems == false)
            {
                MessageBox.Show("Veuillez d'abord paramétrer les Départements de cet entreprise !", "Ajout Salarié", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            bEtat  = 1;
            this.Resources["bSalEdit"] = true;
            this.Resources["_bSalEdit"] = false;
            cmb_npaville.ItemsSource = null;
            

            utilitaire.Salarie newitem = new utilitaire.Salarie();
            newitem.identreprise = (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).identreprise;
            g_contrat.SelectedIndex = -1;
            g_contrat.ItemsSource = null;
            g_selsalarie.DataContext = newitem;
        }

        private void bt_sal_modifier_Click(object sender, RoutedEventArgs e)
        {
            bEtat = 2;
            this.Resources["_bSalEdit"] = false;
            this.Resources["bSalEdit"] = true;
            this.Resources["_bContEdit"] = false;
        }

        private void bt_sal_valider_Click(object sender, RoutedEventArgs e)
        {
            if (testerchamp(p_adresse))
            {

                return;
            }
            if (bEtat == 1)
            {
                miseajour(p_adresse);
                //miseajour(p_identite);
                if (UT.enregistrerSalarie(g_selsalarie.DataContext as utilitaire.Salarie, 1) == true)
                {
                    System.Windows.Forms.MessageBox.Show("Ajout éffectué avec succès !", "Ajout d'employé", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    UT.salaries.Add(g_selsalarie.DataContext as utilitaire.Salarie);
                    dataGrid.ItemsSource = cmb_grille_salarie.ItemsSource = null;
                    //cmb_grille_salarie.ItemsSource = null;

                }
                else
                    System.Windows.Forms.MessageBox.Show("Il y a eu une erreur !", "Ajout d'employé", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                afficheDonneesEntreprise();
                //dataGrid.SelectedItem = g_selsalarie.DataContext as utilitaire.Salarie;
            }
            else if (bEtat == 2)
            {
                miseajour(p_adresse);
                //miseajour(p_identite);
                if (UT.enregistrerSalarie(g_selsalarie.DataContext as utilitaire.Salarie, 2) == true)
                {
                    System.Windows.Forms.MessageBox.Show("Modification éffectuée avec succès !", "Modification d'employé", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    //UT.salaries.Add(g_selsalarie.DataContext as utilitaire.Salarie);
                }
                else
                    System.Windows.Forms.MessageBox.Show("Il y a eu une erreur !", "Modification d'employé", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                afficheDonneesEntreprise(dataGrid.SelectedIndex);
                //dataGrid.SelectedItem = g_selsalarie.DataContext as utilitaire.Salarie;

            }


            bEtat = 0;
            this.Resources["_bSalEdit"] = true;
            this.Resources["bSalEdit"] = false;
        }

        private void bt_sal_annuler_Click(object sender, RoutedEventArgs e)
        {
            bEtat = 0;
            //this.Resources["_bSalEdit"] = true;
            this.Resources["bSalEdit"] = false;
            //this.Resources["bContEdit"] = false;
            g_selsalarie.DataContext = null;
            //g_selsalarie.DataContext = (dataGrid.SelectedItem as utilitaire.Salarie);
            if (dataGrid.SelectedIndex == -1)
                this.Resources["_bSalEdit"] = false;
            else
                this.Resources["_bSalEdit"] = true;
            afficheSalarie();

        }

        private void bt_sal_suppr_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult rep = System.Windows.Forms.MessageBox.Show("Etes-vous sûre de supprimer ce Salarié ?", "Gestion Salariés", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (rep == System.Windows.Forms.DialogResult.Yes)
            {
                UT.enregistrerSalarie(g_selsalarie.DataContext as utilitaire.Salarie, 3);
                UT.salaries.Remove(g_selsalarie.DataContext as utilitaire.Salarie);
                afficheDonneesEntreprise();
            }
        }

        private void cmb_Entreprise_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            
            //if (cmb_Entreprise.SelectedIndex == -1)
            //   this.Resources["_bEntEdit"] = false;
        }

        private void g_contrat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (g_contrat.SelectedIndex == -1)
            {
                this.Resources["_bContEdit"] = false;
            }
            else
                this.Resources["_bContEdit"] = true;
            //p_identitedetail.DataContext = (g_contrat.SelectedItem as utilitaire.Contrat);
            affichecontrat();
        }

        private void bt_cont_ajout_Click(object sender, RoutedEventArgs e)
        {
            bEtatCont = 1;
            this.Resources["bContEdit"] = true;
            this.Resources["_bContEdit"] = false;
            utilitaire.Contrat newitem = new utilitaire.Contrat();
            newitem.idsalarie = (g_selsalarie.DataContext as utilitaire.Salarie).idsalaries;
            p_identitedetail.DataContext = newitem;
        }

        private void bt_cont_modif_Click(object sender, RoutedEventArgs e)
        {
            bEtatCont = 2;
            this.Resources["bContEdit"] = true;
            this.Resources["_bContEdit"] = false;
        }

        private void bt_cont_suppr_Click(object sender, RoutedEventArgs e)
        {
            //bEtatCont = 3;
            System.Windows.Forms.DialogResult rep = System.Windows.Forms.MessageBox.Show("Etes-vous sûre de supprimer ce Contrat ?", "Gestion Contrat Salarié", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (rep == System.Windows.Forms.DialogResult.Yes)
            {
                UT.enregistrerContrat(p_identitedetail.DataContext as utilitaire.Contrat, 3);
                //UT.contrats.Remove(p_identitedetail.DataContext as utilitaire.Contrat);

                //affichecontrat();
                afficheDonneesEntreprise();
            }
        }

        private void bt_cont_valid_Click(object sender, RoutedEventArgs e)
        {
            if (testerchamp(p_identitedetail))
            {

                return;
            }

            if (bEtatCont == 1)
            {
                
                miseajour(p_identitedetail);
                if (UT.enregistrerContrat(p_identitedetail.DataContext as utilitaire.Contrat, 1) == true)
                {
                    System.Windows.Forms.MessageBox.Show("Ajout éffectué avec succès !", "Ajout de contrat", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    //UT.contrats.Add(p_identitedetail.DataContext as utilitaire.Contrat);
                    afficheDonneesEntreprise(dataGrid.SelectedIndex);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Il y a eu une erreur !", "Ajout de contrat", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    //this.Resources["bInsEdit"] = false;
                    return;
                }
            }
            else if (bEtatCont == 2)
            {
                miseajour(p_identitedetail);
                if (UT.enregistrerContrat(p_identitedetail.DataContext as utilitaire.Contrat, 2) == true)
                {
                    System.Windows.Forms.MessageBox.Show("Modification éffectuée avec succès !", "Modification de contrat", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    //UT.salaries.Add(g!v_selsalarie.DataContext as utilitaire.Salarie);
                    afficheDonneesEntreprise(dataGrid.SelectedIndex);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Il y a eu une erreur !", "Modification de contrat", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return;
                }
            }
            this.Resources["bContEdit"] = false;
            /*int sel = -1;
            if (bEtatCont == 1)
            {
                g_contrat.ItemsSource = null;
                g_contrat.ItemsSource = UT.contrats;
                g_contrat.SelectedItem = p_identitedetail.DataContext as utilitaire.Contrat;
            }
            else
            {
                sel = g_contrat.SelectedIndex;
                g_contrat.ItemsSource = null;
                g_contrat.ItemsSource = UT.contrats;
                g_contrat.SelectedIndex = sel;
            }*/
            bEtatCont = 0;
        }

        private void affichecontrat()
        {
            p_identitedetail.DataContext = null;
            //if (g_contrat.SelectedIndex > -1)
            if (g_contrat.SelectedIndex == -1)
            {
                if (g_contrat.HasItems)
                {
                    g_contrat.SelectedIndex = 0;
                    return;
                }
                this.Resources["_bContEdit"] = false;
                this.Resources["bContEdit"] = false;
            }
            else
            {
                this.Resources["_bContEdit"] = true;
                this.Resources["bContEdit"] = false;
            }
            p_identitedetail.DataContext = (g_contrat.SelectedItem as utilitaire.Contrat);
        }
        private void bt_cont_annul_Click(object sender, RoutedEventArgs e)
        {
            this.Resources["bContEdit"] = false;
            p_identitedetail.DataContext = null;
            affichecontrat();
        }

        private void bt_cont_encours_Click(object sender, RoutedEventArgs e)
        {
            if (UT.enregistrerContratEncours(p_identitedetail.DataContext as utilitaire.Contrat) == true)
            {
                System.Windows.Forms.MessageBox.Show("Le contrat sélectionné est bien défini !", "Contrat", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                afficheDonneesEntreprise(dataGrid.SelectedIndex);
            }
        }

        private void cmb_anneetaux_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void ed_tx_annee_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (ed_tx_annee.Text.Trim() == "")
                return;
            afficherTauxEnt(ed_tx_annee.Text);
            //IEnumerable<utilitaire.TauxEntreprise> res = UT.TauxEntreprises.Where(w => w.annee == ed_tx_annee.Text);
            //p_tauxentreprise.DataContext = gv_tauxentreprise.ItemsSource = res;
        }

        private void affichetauxdetail()
        {
            p_tauxentreprise.DataContext = null;
            p_tauxentreprise.DataContext = gv_tauxentreprise.SelectedItem;
            this.Resources["bTauxEdit"] = false;
            this.Resources["_bTauxEdit"] = true;
        }
        private void gv_tauxentreprise_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            affichetauxdetail();
        }

        private void bt_taux_ajout_Click(object sender, RoutedEventArgs e)
        {
            bEtatTaux = 1;
            this.Resources["bTauxEdit"] = true;
            this.Resources["_bTauxEdit"] = false;
            ed_tx_deduction.Text = "1";
            utilitaire.TauxEntreprise newitem = new utilitaire.TauxEntreprise();
            newitem.identreprise = (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).identreprise;
            newitem.annee = ed_tx_annee.Text.Trim();
            p_tauxentreprise.DataContext = newitem;
        }

        private void bt_taux_modif_Click(object sender, RoutedEventArgs e)
        {
            bEtatTaux = 2;
            this.Resources["bTauxEdit"] = true;
            this.Resources["_bTauxEdit"] = false;
        }

        private void bt_taux_suppr_Click(object sender, RoutedEventArgs e)
        {
            //bEtatCont = 3;
            System.Windows.Forms.DialogResult rep = System.Windows.Forms.MessageBox.Show("Etes-vous sûre de supprimer cette ligne de Taux ?", "Gestion Taux d'entreprise", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (rep == System.Windows.Forms.DialogResult.Yes)
            {
                UT.enregistrerTauxEntreprise(p_tauxentreprise.DataContext as utilitaire.TauxEntreprise, 3);
                UT.TauxEntreprises.Remove(gv_tauxentreprise.SelectedItem as utilitaire.TauxEntreprise);

                //affichecontrat();
                afficherTauxEnt(ed_tx_annee.Text.Trim());
            }
        }

        private void bt_taux_valid_Click(object sender, RoutedEventArgs e)
        {
            if (testerchamp(p_tauxentreprise))
            {

                return;
            }

            if (bEtatTaux == 1)
            {
                //(p_tauxentreprise.DataContext as utilitaire.TauxEntreprise).instnom = (cmb_tx_inst.SelectedItem as utilitaire.Institution).societe;
                miseajour(p_tauxentreprise);
                int ret = UT.enregistrerTauxEntreprise(p_tauxentreprise.DataContext as utilitaire.TauxEntreprise, 1);
                if (ret > 0)
                {
                    System.Windows.Forms.MessageBox.Show("Ajout éffectué avec succès !", "Ajout de ligne de Taux pour Base de calcul", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    (p_tauxentreprise.DataContext as utilitaire.TauxEntreprise).idbasecalcul = ret.ToString();
                    UT.TauxEntreprises.Add(p_tauxentreprise.DataContext as utilitaire.TauxEntreprise);
                    afficherTauxEnt(ed_tx_annee.Text.Trim());
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Il y a eu une erreur !", "Ajout de ligne de Taux pour Base de calcul", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    //this.Resources["bInsEdit"] = false;
                    return;
                }
            }
            else if (bEtatTaux == 2)
            {
                //(p_tauxentreprise.DataContext as utilitaire.TauxEntreprise).instnom = (cmb_tx_inst.SelectedItem as utilitaire.Institution).societe;
                miseajour(p_tauxentreprise);
                if (UT.enregistrerTauxEntreprise(p_tauxentreprise.DataContext as utilitaire.TauxEntreprise, 2) > 0)
                {
                    //(gv_tauxentreprise.SelectedItem as utilitaire.TauxEntreprise).instnom = (cmb_tx_inst.SelectedItem as utilitaire.Institution).societe;

                    System.Windows.Forms.MessageBox.Show("Modification éffectuée avec succès !", "Modification de ligne de Taux pour Base de calcul", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    //UT.salaries.Add(g!v_selsalarie.DataContext as utilitaire.Salarie);
                    afficherTauxEnt(ed_tx_annee.Text.Trim());
                    //affichetauxdetail();
                    //afficheDonneesEntreprise(dataGrid.SelectedIndex);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Il y a eu une erreur !", "Modification de contrat", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return;
                }
            }
            this.Resources["bTauxEdit"] = false;
            /*int sel = -1;
            if (bEtatCont == 1)
            {
                g_contrat.ItemsSource = null;
                g_contrat.ItemsSource = UT.contrats;
                g_contrat.SelectedItem = p_identitedetail.DataContext as utilitaire.Contrat;
            }
            else
            {
                sel = g_contrat.SelectedIndex;
                g_contrat.ItemsSource = null;
                g_contrat.ItemsSource = UT.contrats;
                g_contrat.SelectedIndex = sel;
            }*/
            bEtatTaux = 0;
        }

        private void bt_taux_annul_Click(object sender, RoutedEventArgs e)
        {
            this.Resources["bTauwEdit"] = false;
            p_tauxentreprise.DataContext = null;
            affichetauxdetail();
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*if (tabControl.SelectedIndex == 2) //tabDonneebase
            {
                chargerDonneebase();
            }*/
        }

        private void ChargerTauxcopie()
        {
            UT.TauxEntreprisesCopy.Clear();
            foreach (utilitaire.TauxEntreprise d in UT.TauxEntreprises)
            {
                utilitaire.TauxEntrepriseCopy ff = new utilitaire.TauxEntrepriseCopy();
                ff.annee = d.annee;
                ff.conditioncalc = d.conditioncalc;
                ff.deduction = d.deduction;
                ff.formulebase = d.formulebase;
                ff.idbasecalcul = d.idbasecalcul;
                ff.identreprise = d.identreprise;
                ff.idinstitution = d.idinstitution;
                ff.instnom = d.instnom;
                ff.comptecompta = d.comptecompta;
                ff.rang = d.rang;
                ff.selectionne = false;
                ff.tauxemploye = d.tauxemploye;
                ff.tauxemployeur = d.tauxemployeur;
                ff.txtparam = d.txtparam;
                ff.typeparam = d.typeparam;
                UT.TauxEntreprisesCopy.Add(ff);
            }
        }

        private void chargerDonneebase()
        {
            /*IEnumerable<utilitaire.TauxEntreprise> result;
            result = UT.TauxEntreprises.Where(w => w.idsa.IndexOf(ed_inst_npacity.Text) == 0);*/
            gv_donneebasesalarie.ItemsSource = null;
            gv_donneebasesalariepercue.ItemsSource = null;
            gv_donneebasesalariepercue.Items.Clear();
            /*ListCollectionView gg = new ListCollectionView(UT.TauxEntreprises);
            gg.GroupDescriptions.Add(new PropertyGroupDescription("typeparam"));*/

            ChargerTauxcopie();

            gv_donneebasesalarie.ItemsSource =  UT.TauxEntreprisesCopy;
            UT.chargerDonneeBaseSalaries((dataGrid.SelectedItem as utilitaire.Salarie).idsalaries);
            cmb_bd_genrelpp.ItemsSource = UT.GenreLPPs;

            //IEnumerable<IGrouping<string, string>> tauxannee = res.OrderBy(w => w.rang).GroupBy(w => w.identreprise, w => w.typeparam);
            IEnumerable<utilitaire.DonneeBaseSalarie> res = UT.DonneeBaseSalaries.Where(w => w.idsalarie == (dataGrid.SelectedItem as utilitaire.Salarie).idsalaries && w.code == "LPP");
            bool blpp = false;
            if (res.Count() > 0)
            {
                //ed_mtt_sallppbase.Text = UT.GetDecimalSql(res.First().basecalc);
                ed_mtt_sallpptauxemploye.Text = UT.GetDecimalSql(res.First().partemploye);
                cmb_bd_genrelpp.SelectedValue = res.First().idbasecalcul;
                ed_mtt_sallpprepartition.Text = res.First().repartition;
                blpp = true;
            }
            IEnumerable<utilitaire.DonneeBaseSalarie> curpercu = UT.DonneeBaseSalaries.Where(w => w.idsalarie == (dataGrid.SelectedItem as utilitaire.Salarie).idsalaries && (w.deduction == "0" || w.deduction == "+") && w.idbasecalcul != "");
            foreach(utilitaire.DonneeBaseSalarie lignepercu in curpercu)
            {
                utilitaire.TauxEntrepriseCopy db = new DeltaSalaire.utilitaire.TauxEntrepriseCopy();
                db.deduction = lignepercu.deduction;
                db.formulebase = lignepercu.basecalc;
                db.idbasecalcul = lignepercu.idbasecalcul;
                db.rang = lignepercu.rang;
                db.selectionne = true;
                db.tauxemploye = lignepercu.partemploye;
                db.tauxemployeur = "0";
                db.txtparam= UT.TauxEntreprises.Where(w => w.idbasecalcul == lignepercu.idbasecalcul).First().txtparam;
                db.typeparam = lignepercu.code;
                gv_donneebasesalariepercue.Items.Add(db);
            }
            foreach (object ff in gv_donneebasesalarie.Items)
            {
                (ff as utilitaire.TauxEntrepriseCopy).selectionne = false;
                IEnumerable<utilitaire.DonneeBaseSalarie> cur = UT.DonneeBaseSalaries.Where(w => w.idsalarie == (dataGrid.SelectedItem as utilitaire.Salarie).idsalaries && w.code == (ff as utilitaire.TauxEntrepriseCopy).typeparam && (w.deduction == "1" || w.deduction == "-"));

                if (cur.Count() > 0)
                {
                    (ff as utilitaire.TauxEntrepriseCopy).formulebase = cur.First().basecalc;

                    //if (UT.DonneeBaseSalaries.Count(w => w.idbasecalcul == (ff as utilitaire.TauxEntrepriseCopy).idbasecalcul && w.basecalc == "" && w.basecalc.Contains("LPP") == false) > 0)
                    if ((ff as utilitaire.TauxEntrepriseCopy).idbasecalcul == cur.First().idbasecalcul && (ff as utilitaire.TauxEntrepriseCopy).typeparam != "LPP")
                    {
                        (ff as utilitaire.TauxEntrepriseCopy).selectionne = true;

                    }
                    if ((ff as utilitaire.TauxEntrepriseCopy).typeparam == "LPP" && blpp)
                    {
                        (ff as utilitaire.TauxEntrepriseCopy).selectionne = true;
                        (ff as utilitaire.TauxEntrepriseCopy).formulebase = res.First().basecalc;
                        (ff as utilitaire.TauxEntrepriseCopy).tauxemploye = res.First().partemploye;

                    }
                }
            }
            ed_mtt_salairemens.Text = "0.00";
            decimal sal12 = 0;
            res = UT.DonneeBaseSalaries.Where(w => w.idsalarie == (dataGrid.SelectedItem as utilitaire.Salarie).idsalaries && w.code == "SALMENS");
            ed_mtt_repartitionmois.Text = "12";
            if (res.Count() > 0)
            {
                this.Resources["bBaseCalc"] = true;
                this.Resources["_bBaseCalc"] = false;
                ed_mtt_repartitionmois.Text = res.First().repartition;
                //ed_mtt_salairemens.Text = GetForma(UT.ToDecimal(res.First().basecalc) / 12);
                ed_mtt_salairemens.Text = GetForma(UT.ToDecimal(res.First().basecalc));
                sal12 = UT.ToDecimal(res.First().basecalc) * 12;
            }
            else
            {
                this.Resources["bBaseCalc"] = false;
                this.Resources["_bBaseCalc"] = true;
            }
                ed_mtt_treizieme.Text = "0.00";
            ed_mtt_salannu.Text = "0.00";
            res = UT.DonneeBaseSalaries.Where(w => w.idsalarie == (dataGrid.SelectedItem as utilitaire.Salarie).idsalaries && w.code == "SALANNU");
            if (res.Count() > 0)
            {
                //ed_mtt_repartitionannee.Text = res.First().repartition;
                ed_mtt_salannu.Text = UT.GetDecimalSql(res.First().basecalc);
                ed_mtt_treizieme.Text = UT.GetDecimalSql((UT.ToDecimal(res.First().basecalc) - sal12).ToString());
            }
            

        }

        private string Getdecimal( string s)
        {
            if (s == "" || s == null)
                s = "0";
            decimal rr = 0;
            try
            {
                rr = decimal.Parse(s);
            }
            catch
            {
                rr = decimal.Parse(s.Replace(".", ","));
            }
            return string.Format("{0:0.00;-0.00;0.00}", rr);
        }
        private void calculTauxLPP(utilitaire.Salarie sal)
        {
            ed_mtt_sallppbase.Text = ed_mtt_sallpptauxemploye.Text = ed_mtt_sallpptauxemployeur.Text = "0.00";
            if (cmb_bd_genrelpp.SelectedIndex == -1)
                return;

            string partemployeur = "0.00";
            string partemploye = "0.00";
            string daty = sal.datenaissance.ToString();
            if (daty != "" && sal.datenaissance.GetType() != typeof(DBNull))
            {
                foreach (utilitaire.Classeage ca in UT.classeages)
                {
                    string sex = sal.sexe;
                    if (sex == "Masculin")
                        sex = "M";
                    else
                        sex = "F";


                    if (int.Parse(ca.deannee) <= DateTime.Parse(sal.datenaissance).Year && int.Parse(ca.aannee) >= DateTime.Parse(sal.datenaissance).Year && ca.sexe.Contains(sex))
                    {
                        partemployeur = ca.partemployeur;
                        partemploye = ca.partemploye;
                        break;
                    }
                }
            }
            decimal difference = 0;
            decimal salaireann = 0;
            decimal deduct = 0;
            salaireann = UT.ToDecimal(ed_mtt_salairemens.Text.Trim()) * 12 + UT.ToDecimal(ed_mtt_treizieme.Text.Trim());
            //decimal.TryParse(Getdecimal(UT.ToDecimal(ed_mtt_salairemens.Text.Trim())*12 + UT.ToDecimal(ed_mtt_treizieme.Text.Trim())), out salaireann);
            decimal.TryParse(Getdecimal((cmb_bd_genrelpp.SelectedItem as utilitaire.GenreLPP).deductioncoord), out deduct);
            difference = salaireann - deduct;
            
            decimal salaireassmax = 0;
            decimal.TryParse(Getdecimal((cmb_bd_genrelpp.SelectedItem as utilitaire.GenreLPP).salaireassuremax), out salaireassmax);
            decimal baselpp = 0;
            if (difference > salaireassmax)
                baselpp = salaireassmax;
            else
                baselpp = difference;

            ed_mtt_sallppbase.Text = Getdecimal(baselpp.ToString());
            ed_mtt_sallpptauxemploye.Text = partemploye;
            ed_mtt_sallpptauxemployeur.Text = partemployeur;
        }

        private void bt_bd_valider_Click(object sender, RoutedEventArgs e)
        {
            string sl = "DELETE FROM " + UT.dbase + ".paie_basesalarie WHERE idsalarie = " + (dataGrid.SelectedItem as utilitaire.Salarie).idsalaries;
            UT.supprimerDonneeBaseSalarie((dataGrid.SelectedItem as utilitaire.Salarie).idsalaries);
            UT.enregistrerGrillesalarie(null, 3, true, (dataGrid.SelectedItem as utilitaire.Salarie).idsalaries);
            UT.enregistrerPaiement(null, 3, true, (dataGrid.SelectedItem as utilitaire.Salarie).idsalaries);
            utilitaire.DonneeBaseSalarie newB = new utilitaire.DonneeBaseSalarie();
            string rep = "12";
            newB.idsalarie = (dataGrid.SelectedItem as utilitaire.Salarie).idsalaries;
            newB.code = "SALMENS";
            newB.rang = "1";
            newB.deduction = "0";
            if (UT.ToDecimal(ed_mtt_treizieme.Text.Trim()) > 0)
                rep = "13";
                newB.repartition = rep;
            //else
            //    newB.repartition = ed_mtt_repartitionmois.Text.Trim();
            newB.basecalc = string.Format("{0:0.00;-0.00;0.00}", UT.ToDecimal(ed_mtt_salairemens.Text.Trim())).Replace(",", "."); // UT.GetDecimalSql((UT.ToDecimal(ed_mtt_salairemens.Text.Trim()) * 12 + UT.ToDecimal(ed_mtt_treizieme.Text.Trim()))); // UT.ToDecimal(newB.repartition)).ToString());
            UT.enregistrerDonneeBaseSalarie(newB, 1);
            newB = new utilitaire.DonneeBaseSalarie();
            newB.idsalarie = (dataGrid.SelectedItem as utilitaire.Salarie).idsalaries;
            newB.code = "SALANNU";
            newB.rang = "2";
            newB.deduction = "0";
            //if (ed_mtt_repartitionannee.Text.Trim() == "")
                newB.repartition = rep;
            //else
            //    newB.repartition = ed_mtt_repartitionannee.Text.Trim();
            //newB.basecalc = UT.GetDecimalSql(ed_mtt_salaireannuel.Text.Trim());
            newB.basecalc = string.Format("{0:0.00;-0.00;0.00}", UT.ToDecimal(ed_mtt_salairemens.Text.Trim()) * 12 + UT.ToDecimal(ed_mtt_treizieme.Text.Trim())).Replace(",", "."); // UT.GetDecimalSql((UT.ToDecimal(ed_mtt_salairemens.Text.Trim()) * 12 + UT.ToDecimal(ed_mtt_treizieme.Text.Trim()))); // UT.ToDecimal(newB.repartition)).ToString());
            UT.enregistrerDonneeBaseSalarie(newB, 1);
            int ii = 1;
            foreach (object ff in gv_donneebasesalariepercue.Items)
            {
                utilitaire.TauxEntrepriseCopy gg = ff as utilitaire.TauxEntrepriseCopy;
                if (gg != null && gg.selectionne == true)
                {
                    newB = new utilitaire.DonneeBaseSalarie();
                    newB.idsalarie = (dataGrid.SelectedItem as utilitaire.Salarie).idsalaries;
                    
                        newB.idbasecalcul = gg.idbasecalcul;
                        newB.deduction = "0";
                        newB.partemploye = UT.GetDecimal3VirglSql(gg.tauxemploye);
                        newB.partemployeur = UT.GetDecimal3VirglSql(gg.tauxemployeur);
                        newB.rang = (ii + 2).ToString();
                        newB.basecalc = gg.formulebase;
                        newB.code = gg.typeparam;
                        //if (ed_mtt_repartitionannee.Text.Trim() == "")
                        newB.repartition = rep;
                        //else
                        //    newB.repartition = ed_mtt_repartitionannee.Text.Trim();
                    
                    UT.enregistrerDonneeBaseSalarie(newB, 1);
                    ii++;
                }
            }
            foreach (object ff in gv_donneebasesalarie.Items)
            {
                utilitaire.TauxEntrepriseCopy gg = ff as utilitaire.TauxEntrepriseCopy;
                if (gg != null && gg.selectionne == true)
                {
                    newB = new utilitaire.DonneeBaseSalarie();
                    newB.idsalarie = (dataGrid.SelectedItem as utilitaire.Salarie).idsalaries;
                    if (gg.typeparam.Contains("LPP"))
                    {
                        newB.idbasecalcul = (cmb_bd_genrelpp.SelectedItem as utilitaire.GenreLPP).idcalcullpp;
                        newB.deduction = gg.deduction;
                        newB.partemploye = UT.GetDecimal3VirglSql(ed_mtt_sallpptauxemploye.Text);
                        newB.partemployeur = UT.GetDecimal3VirglSql(ed_mtt_sallpptauxemployeur.Text);
                        newB.basecalc = UT.GetDecimalSql(ed_mtt_sallppbase.Text.Trim());
                        newB.code = "LPP";
                        //newB.rang = (int.Parse(gg.rang) + gv_donneebasesalariepercue.Items.Count + 2).ToString();
                        newB.rang = (ii + 2).ToString();
                        //if (ed_mtt_sallpprepartition.Text.Trim() == "")
                            newB.repartition = rep;
                        //else
                        //    newB.repartition = ed_mtt_sallpprepartition.Text.Trim();
                    }
                    else
                    {
                        newB.idbasecalcul = gg.idbasecalcul;
                        newB.deduction = gg.deduction;
                        newB.partemploye = UT.GetDecimal3VirglSql(gg.tauxemploye);
                        newB.partemployeur = UT.GetDecimal3VirglSql(gg.tauxemployeur);
                        //newB.rang = (int.Parse(gg.rang) + gv_donneebasesalariepercue.Items.Count + 2).ToString();
                        newB.rang = (ii + 2).ToString();
                        newB.basecalc = gg.formulebase;
                        newB.code = gg.typeparam;
                        //if (ed_mtt_repartitionannee.Text.Trim() == "")
                            newB.repartition = rep;
                        //else
                        //    newB.repartition = ed_mtt_repartitionannee.Text.Trim();
                    }
                    UT.enregistrerDonneeBaseSalarie(newB, 1);
                    ii++;
                }
            }
            chargerDonneebase();
            UT.chargerDonneeSalarieResume((dataGrid.SelectedItem as utilitaire.Salarie).idsalaries);
            gv_donneeresume.ItemsSource = UT.DonneeSalarieResumes;
            cmb_grille_salarie.SelectedIndex = -1;
            System.Windows.Forms.MessageBox.Show("Validation de la Base de Calcul Salarié éffectuée avec succès !", "Taux pour Base de calcul Salarié", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
        }
        private void bt_bd_reinitialiser_Click(object sender, RoutedEventArgs e)
        {
            UT.supprimerDonneeBaseSalarie((dataGrid.SelectedItem as utilitaire.Salarie).idsalaries);
            UT.enregistrerGrillesalarie(null, 3, true, (dataGrid.SelectedItem as utilitaire.Salarie).idsalaries);
            UT.enregistrerPaiement(null, 3, true, (dataGrid.SelectedItem as utilitaire.Salarie).idsalaries);
            chargerDonneebase();
            UT.chargerDonneeSalarieResume((dataGrid.SelectedItem as utilitaire.Salarie).idsalaries);
            gv_donneeresume.ItemsSource = UT.DonneeSalarieResumes;
        }

        private void gv_donneebasesalarie_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            /*if (gv_donneebasesalarie.HasItems)
            {
                if (gv_donneebasesalarie.SelectedIndex != -1)
                {
                    if ((gv_donneebasesalarie.SelectedItem as utilitaire.TauxEntreprise).selectionne)
                        (gv_donneebasesalarie.SelectedItem as utilitaire.TauxEntreprise).selectionne = false;
                    else
                       (gv_donneebasesalarie.SelectedItem as utilitaire.TauxEntreprise).selectionne = true;
                }
            }*/
        }

        private void ed_mtt_salairemens_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void ed_lpp_annee_TextChanged(object sender, TextChangedEventArgs e)
        {
            UT.chargerClasseages(ed_lpp_annee.Text.Trim());
            gv_paramlpp_agetaux.ItemsSource = UT.classeages;
            p_paramlpp_classeage.DataContext = null;

            UT.chargerGenreLPP(ed_lpp_annee.Text.Trim());
            gv_paramlpp_genrelpp.ItemsSource = UT.GenreLPPs;
            p_paramlpp_genrelpp.DataContext = null;
        }

        private void gv_paramlpp_agetaux_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            p_paramlpp_classeage.DataContext = null;
            if (gv_paramlpp_agetaux.SelectedIndex > -1)
                p_paramlpp_classeage.DataContext = gv_paramlpp_agetaux.SelectedItem as utilitaire.Classeage;
        }

        private void bt_lpp_ajoutage_Click(object sender, RoutedEventArgs e)
        {
            gv_paramlpp_agetaux.SelectedIndex = -1;
            gv_paramlpp_agetaux.SelectedItem = null;
            utilitaire.Classeage newc = new utilitaire.Classeage();
            p_paramlpp_classeage.DataContext = newc;

        }

        private void enregistrerlppgenre(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (testerchamp(p_paramlpp_genrelpp))
                    return;
                (p_paramlpp_genrelpp.DataContext as utilitaire.GenreLPP).annee = ed_lpp_annee.Text.Trim();
                miseajour(p_paramlpp_genrelpp);
                if ((p_paramlpp_genrelpp.DataContext as utilitaire.GenreLPP).idcalcullpp == "" || (p_paramlpp_genrelpp.DataContext as utilitaire.GenreLPP).idcalcullpp == null) //ajout
                {

                    if (UT.enregistrerGenreLPP(p_paramlpp_genrelpp.DataContext as utilitaire.GenreLPP, 1))
                    {
                        //UT.departements.Add(ed_param_departement.DataContext as utilitaire.Departement);
                        System.Windows.Forms.MessageBox.Show("Ajout Genre LPP éffectué avec succès !", "Paramétrage LPP", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

                        UT.chargerGenreLPP(ed_lpp_annee.Text.Trim());
                        gv_paramlpp_genrelpp.SelectedIndex = -1;
                        gv_paramlpp_genrelpp.SelectedItem = null;
                        p_paramlpp_genrelpp.DataContext = null;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("il y a eu un erreur ! ", "Paramétrage LPP", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    UT.enregistrerGenreLPP(p_paramlpp_genrelpp.DataContext as utilitaire.GenreLPP, 2);
                    int isel = gv_paramlpp_genrelpp.SelectedIndex;
                    gv_paramlpp_genrelpp.SelectedIndex = -1;
                    gv_paramlpp_genrelpp.SelectedItem = null;
                    gv_paramlpp_genrelpp.SelectedIndex = isel;
                }
            }
        }
        private void enregistrerlpptaux(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //if (ed_lpp_tauxemploye.Text.Trim() == "" || ed_lpp_annee.Text.Trim() == "" || ed_lpp_tauxemploye.DataContext == null)
                if (testerchamp(p_paramlpp_classeage))
                        return;
                (p_paramlpp_classeage.DataContext as utilitaire.Classeage).annee = ed_lpp_annee.Text.Trim();
                miseajour(p_paramlpp_classeage);
                if ((p_paramlpp_classeage.DataContext as utilitaire.Classeage).idclasseage == "" || (p_paramlpp_classeage.DataContext as utilitaire.Classeage).idclasseage == null) //ajout
                {

                    if (UT.enregistrerClasseage(p_paramlpp_classeage.DataContext as utilitaire.Classeage, 1))
                    {
                        //UT.departements.Add(ed_param_departement.DataContext as utilitaire.Departement);
                        System.Windows.Forms.MessageBox.Show("Ajout Classe d'Age éffectué avec succès !", "Paramétrage LPP", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

                        UT.chargerClasseages(ed_lpp_annee.Text.Trim());
                        gv_paramlpp_agetaux.SelectedIndex = -1;
                        gv_paramlpp_agetaux.SelectedItem = null;
                        p_paramlpp_classeage.DataContext = null;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("il y a eu un erreur ! ", "Paramétrage LPP", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    UT.enregistrerClasseage(p_paramlpp_classeage.DataContext as utilitaire.Classeage, 2);
                    int isel = gv_paramlpp_agetaux.SelectedIndex;
                    gv_paramlpp_agetaux.SelectedIndex = -1;
                    gv_paramlpp_agetaux.SelectedItem = null;
                    gv_paramlpp_agetaux.SelectedIndex = isel;
                }
            }
        }
        private void ed_lpp_tauxemploye_KeyUp(object sender, KeyEventArgs e)
        {
            enregistrerlpptaux(e);
        }

        private void ed_lpp_tauxemployeur_KeyUp(object sender, KeyEventArgs e)
        {
            enregistrerlpptaux(e);
        }

        private void bt_lpp_supprage_Click(object sender, RoutedEventArgs e)
        {

            System.Windows.Forms.DialogResult rep = System.Windows.Forms.MessageBox.Show("Etes-vous sûre de supprimer cette ligne ?", "Paramétrage Classe d'êge LPP", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (rep == System.Windows.Forms.DialogResult.Yes)
            {
                UT.enregistrerClasseage(p_paramlpp_classeage.DataContext as utilitaire.Classeage, 3);
                UT.chargerClasseages(ed_lpp_annee.Text.Trim());
                gv_paramlpp_agetaux.SelectedItem = null;
            }
        }

        private void gv_paramlpp_genrelpp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            p_paramlpp_genrelpp.DataContext = null;
            if (gv_paramlpp_genrelpp.SelectedIndex > -1)
                p_paramlpp_genrelpp.DataContext = gv_paramlpp_genrelpp.SelectedItem as utilitaire.GenreLPP;
        }

        private void ed_lpp_assuremin_KeyUp(object sender, KeyEventArgs e)
        {
            enregistrerlppgenre(e);
        }

        private void cmb_bd_genrelpp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            calculTauxLPP(dataGrid.SelectedItem as utilitaire.Salarie);
        }

        private void ed_mtt_salaireannuel_TextChanged(object sender, TextChangedEventArgs e)
        {

            calculTauxLPP(dataGrid.SelectedItem as utilitaire.Salarie);
        }

        private string GetForma(object ss)
        {
            if (ss == "")
                ss = 0;
            decimal d = UT.ToDecimal(ss.ToString());
            return string.Format("{0:0.00;-0.00;0.00}", d).Replace(",", ".");
        }
        private string GetFormaDate(object ss)
        {
            if (ss == null)
                ss = "";
            return string.Format("{0:dd.MM.yyyy}", DateTime.Parse(ss.ToString()));
        }

        private string GetFormaRound(object ss)
        {
            if (ss == "")
                ss = 0;
            decimal d = UT.ToDecimal(ss.ToString());
            d = Math.Round(d / UT.ToDecimal("0.05")) * UT.ToDecimal("0.05");
            return string.Format("{0:0.00;-0.00;0.00}", d).Replace(",", ".");
        }
        private void cmb_grille_salarie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            chargergrille();
            //gv_grillesalaire.ItemsSource = UT.Grillesalaries;

        }

        private void chargergrille()
        {
            //p_paiement.DataContext = null;
            ed_grille_mttmodifval.IsEnabled = ck_toutmodifierapartir.IsEnabled = bt_grille_modifvalider.IsEnabled = false;
            this.Resources["bGrilleValide"] = false;
            this.Resources["_bGrilleValide"] = false;
            this.Resources["bMoisValide"] = false;
            bool bgrillev = true;
            UT.Grillesalaries.Clear();
            if (cmb_grille_salarie.SelectedIndex == -1)
                return;
            UT.chargerGrillesalarie(cmb_grille_salarie.SelectedValue.ToString(), ed_tx_annee.Text.Trim(), 2); //salaires déjà validés
            //IEnumerable<utilitaire.Grillesalarie> grillesal = UT.Grillesalaries.Where(w => w.idsalarie == cmb_grille_salarie.SelectedValue.ToString());

            if (UT.Grillesalaries.Count() == 0)
            {
                //pas encore validés
                
                bgrillev = false;
                UT.chargerGrillesalarie(cmb_grille_salarie.SelectedValue.ToString(), ed_tx_annee.Text.Trim(), 1);

            }
            //gv_grillesalaire.Items.Clear();
            decimal ttot = 0;
            decimal ttot_ = 0;
            decimal dval = 0;
            decimal dval_ = 0;
            foreach (utilitaire.Grillesalarie bs in UT.Grillesalaries)
            {
                if (!bgrillev)
                {
                    string val = GetFormaRound(bs.valeur);
                    bs.valeur1 = bs.valeur2 = bs.valeur3 = bs.valeur4 = bs.valeur5 = bs.valeur6 = bs.valeur7 = bs.valeur8 = bs.valeur9 = bs.valeur10 = bs.valeur11 = val;
                    dval = UT.ToDecimal(val) * 11;

                    ttot = UT.ToDecimal(bs.valeur) * UT.ToDecimal(bs.repartition);
                    if (bs.repartition == "12")
                    {
                        bs.valeur12 = GetFormaRound(Getdecimal((ttot - dval).ToString()));
                        bs.treisieme = bs.valeur13 = "0.00";
                    }
                    else if (bs.repartition == "13")
                    {
                        bs.valeur12 = val;
                        bs.valeur13 = GetFormaRound(Getdecimal((ttot - (dval + UT.ToDecimal(val))).ToString())); ;
                        bs.treisieme = bs.valeur13;
                    }
                    bs.totannee = GetForma((UT.ToDecimal(val) * 11) + UT.ToDecimal(bs.valeur12));
                    bs.totalgeneral = GetForma((UT.ToDecimal(val) * 11) + UT.ToDecimal(bs.valeur12) + UT.ToDecimal(bs.valeur13));
                }
               // gv_grillesalaire.Items.Add(bs);
                
            }
            if (UT.Grillesalaries.Where(w => w.code == "SALMENS").Count() > 0)
            {
                utilitaire.Grillesalarie gtot = new DeltaSalaire.utilitaire.Grillesalarie();
                gtot.txtligne = "Total Déductions";
                gtot.idsalarie = cmb_grille_salarie.SelectedValue.ToString();
                gtot.unite = "Chf";
                gtot.annee = ed_tx_annee.Text.Trim();
                gtot.code = "TOTDED";
                decimal stot = UT.Grillesalaries.Where(w => w.deduction == "1" || w.deduction == "-").Sum(w => decimal.Parse(Getdecimal(w.valeur)));
                gtot.valeur = GetForma(stot);
                gtot.valeur1 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "1" || w.deduction == "-").Sum(w => decimal.Parse(Getdecimal(w.valeur1))));
                gtot.valeur2 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "1" || w.deduction == "-").Sum(w => decimal.Parse(Getdecimal(w.valeur2))));
                gtot.valeur3 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "1" || w.deduction == "-").Sum(w => decimal.Parse(Getdecimal(w.valeur3))));
                gtot.valeur4 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "1" || w.deduction == "-").Sum(w => decimal.Parse(Getdecimal(w.valeur4))));
                gtot.valeur5 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "1" || w.deduction == "-").Sum(w => decimal.Parse(Getdecimal(w.valeur5))));
                gtot.valeur6 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "1" || w.deduction == "-").Sum(w => decimal.Parse(Getdecimal(w.valeur6))));
                gtot.valeur7 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "1" || w.deduction == "-").Sum(w => decimal.Parse(Getdecimal(w.valeur7))));
                gtot.valeur8 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "1" || w.deduction == "-").Sum(w => decimal.Parse(Getdecimal(w.valeur8))));
                gtot.valeur9 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "1" || w.deduction == "-").Sum(w => decimal.Parse(Getdecimal(w.valeur9))));
                gtot.valeur10 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "1" || w.deduction == "-").Sum(w => decimal.Parse(Getdecimal(w.valeur10))));
                gtot.valeur11 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "1" || w.deduction == "-").Sum(w => decimal.Parse(Getdecimal(w.valeur11))));
                gtot.valeur12 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "1" || w.deduction == "-").Sum(w => decimal.Parse(Getdecimal(w.valeur12))));

                decimal stot1 = UT.Grillesalaries.Where(w => w.deduction == "1" || w.deduction == "-").Sum(w => decimal.Parse(Getdecimal(w.totannee)));
                gtot.totannee = GetForma(stot1);
                decimal treize = UT.Grillesalaries.Where(w => w.deduction == "1" || w.deduction == "-").Sum(w => decimal.Parse(Getdecimal(w.treisieme)));
                gtot.treisieme = gtot.valeur13 = GetForma(treize);
                decimal totgen = UT.Grillesalaries.Where(w => w.deduction == "1" || w.deduction == "-").Sum(w => decimal.Parse(Getdecimal(w.totalgeneral)));
                gtot.totalgeneral = GetForma(totgen);
                UT.Grillesalaries.Add(gtot);
                //gv_grillesalaire.Items.Add(gtot);


                gtot = new DeltaSalaire.utilitaire.Grillesalarie();
                gtot.txtligne = "Salaire NET";
                gtot.idsalarie = cmb_grille_salarie.SelectedValue.ToString();
                gtot.unite = "Chf";
                gtot.code = "SALNET";
                gtot.annee = ed_tx_annee.Text.Trim();
                gtot.valeur = GetForma((UT.Grillesalaries.Where(w => w.deduction == "0" || w.deduction == "+").Sum(w => decimal.Parse(Getdecimal(w.valeur))) - stot));

                gtot.valeur1 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "0" || w.deduction == "+").Sum(w => decimal.Parse(Getdecimal(w.valeur1))) - UT.Grillesalaries.Where(w => (w.deduction == "1" || w.deduction == "-") && w.code != "TOTDED").Sum(w => decimal.Parse(Getdecimal(w.valeur1))));
                gtot.valeur2 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "0" || w.deduction == "+").Sum(w => decimal.Parse(Getdecimal(w.valeur2))) - UT.Grillesalaries.Where(w => (w.deduction == "1" || w.deduction == "-") && w.code != "TOTDED").Sum(w => decimal.Parse(Getdecimal(w.valeur2))));
                gtot.valeur3 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "0" || w.deduction == "+").Sum(w => decimal.Parse(Getdecimal(w.valeur3))) - UT.Grillesalaries.Where(w => (w.deduction == "1" || w.deduction == "-") && w.code != "TOTDED").Sum(w => decimal.Parse(Getdecimal(w.valeur3))));
                gtot.valeur4 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "0" || w.deduction == "+").Sum(w => decimal.Parse(Getdecimal(w.valeur4))) - UT.Grillesalaries.Where(w => (w.deduction == "1" || w.deduction == "-") && w.code != "TOTDED").Sum(w => decimal.Parse(Getdecimal(w.valeur4))));
                gtot.valeur5 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "0" || w.deduction == "+").Sum(w => decimal.Parse(Getdecimal(w.valeur5))) - UT.Grillesalaries.Where(w => (w.deduction == "1" || w.deduction == "-") && w.code != "TOTDED").Sum(w => decimal.Parse(Getdecimal(w.valeur5))));
                gtot.valeur6 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "0" || w.deduction == "+").Sum(w => decimal.Parse(Getdecimal(w.valeur6))) - UT.Grillesalaries.Where(w => (w.deduction == "1" || w.deduction == "-") && w.code != "TOTDED").Sum(w => decimal.Parse(Getdecimal(w.valeur6))));
                gtot.valeur7 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "0" || w.deduction == "+").Sum(w => decimal.Parse(Getdecimal(w.valeur7))) - UT.Grillesalaries.Where(w => (w.deduction == "1" || w.deduction == "-") && w.code != "TOTDED").Sum(w => decimal.Parse(Getdecimal(w.valeur7))));
                gtot.valeur8 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "0" || w.deduction == "+").Sum(w => decimal.Parse(Getdecimal(w.valeur8))) - UT.Grillesalaries.Where(w => (w.deduction == "1" || w.deduction == "-") && w.code != "TOTDED").Sum(w => decimal.Parse(Getdecimal(w.valeur8))));
                gtot.valeur9 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "0" || w.deduction == "+").Sum(w => decimal.Parse(Getdecimal(w.valeur9))) - UT.Grillesalaries.Where(w => (w.deduction == "1" || w.deduction == "-") && w.code != "TOTDED").Sum(w => decimal.Parse(Getdecimal(w.valeur9))));
                gtot.valeur10 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "0" || w.deduction == "+").Sum(w => decimal.Parse(Getdecimal(w.valeur10))) - UT.Grillesalaries.Where(w => (w.deduction == "1" || w.deduction == "-") && w.code != "TOTDED").Sum(w => decimal.Parse(Getdecimal(w.valeur10))));
                gtot.valeur11 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "0" || w.deduction == "+").Sum(w => decimal.Parse(Getdecimal(w.valeur11))) - UT.Grillesalaries.Where(w => (w.deduction == "1" || w.deduction == "-") && w.code != "TOTDED").Sum(w => decimal.Parse(Getdecimal(w.valeur11))));
                gtot.valeur12 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "0" || w.deduction == "+").Sum(w => decimal.Parse(Getdecimal(w.valeur12))) - UT.Grillesalaries.Where(w => (w.deduction == "1" || w.deduction == "-") && w.code != "TOTDED").Sum(w => decimal.Parse(Getdecimal(w.valeur12))));

                gtot.totannee = GetForma(UT.Grillesalaries.Where(w => w.deduction == "0" || w.deduction == "+").Sum(w => decimal.Parse(Getdecimal(w.totannee))) - stot1);
                gtot.treisieme = gtot.valeur13 = GetForma(UT.Grillesalaries.Where(w => w.deduction == "0" || w.deduction == "+").Sum(w => decimal.Parse(Getdecimal(w.treisieme))) - treize);
                gtot.totalgeneral = GetForma(UT.Grillesalaries.Where(w => w.deduction == "0" || w.deduction == "+").Sum(w => decimal.Parse(Getdecimal(w.totalgeneral))) - totgen);
                
                UT.Grillesalaries.Add(gtot);

                //Paiement par mois
                UT.chargerPaiement("", cmb_grille_salarie.SelectedValue.ToString(), ed_tx_annee.Text.Trim(), "");

                utilitaire.Grillesalarie gpaie = new DeltaSalaire.utilitaire.Grillesalarie();
                gpaie.txtligne = "Payé";
                gpaie.unite = "Chf";
                gpaie.idsalarie = cmb_grille_salarie.SelectedValue.ToString();
                gpaie.code = "PAYE";
                gpaie.annee = ed_tx_annee.Text.Trim();
                gpaie.valeur = gpaie.valeur1 = gpaie.valeur2 = gpaie.valeur3 = gpaie.valeur4 = gpaie.valeur5 = gpaie.valeur6 = gpaie.valeur7 = gpaie.valeur8 = gpaie.valeur9 = gpaie.valeur10 = gpaie.valeur11 = gpaie.valeur12 = gpaie.valeur13 = gpaie.treisieme = "0.00";
                utilitaire.Grillesalarie gdatepaie = new DeltaSalaire.utilitaire.Grillesalarie();
                gdatepaie.txtligne = "Date paiement";
                gdatepaie.idsalarie = cmb_grille_salarie.SelectedValue.ToString();
                gdatepaie.unite = "";
                gdatepaie.code = "DATEPAIE";
                gdatepaie.annee = ed_tx_annee.Text.Trim();
                gdatepaie.valeur = gdatepaie.valeur1 = gdatepaie.valeur2 = gdatepaie.valeur3 = gdatepaie.valeur4 = gdatepaie.valeur5 = gdatepaie.valeur6 = gdatepaie.valeur7 = gdatepaie.valeur8 = gdatepaie.valeur9 = gdatepaie.valeur10 = gdatepaie.valeur11 = gdatepaie.valeur12 = gdatepaie.valeur13 = gdatepaie.treisieme = "-";

                decimal totan = 0;
                Application.Current.Resources["imoispaye"] = 0;
                foreach (utilitaire.PaiementSalaire paiesal in UT.PaiementSalaires)
                {
                    totan += paiesal.valeur;
                    if (paiesal.mois == "1")
                    {
                        gpaie.valeur = gpaie.valeur1 = GetForma(paiesal.valeur);
                        if (GetForma(paiesal.valeur) != "0.00")
                            Application.Current.Resources["imoispaye"] = 1;
                            //this.Resources["imoispaye"] = 1;
                        gdatepaie.valeur = gdatepaie.valeur1 = GetFormaDate(paiesal.datepaiement);
                    }
                    if (paiesal.mois == "2")
                    {
                        gpaie.valeur2 = GetForma(paiesal.valeur);
                        if (GetForma(paiesal.valeur) != "0.00")
                            Application.Current.Resources["imoispaye"] = 2;
                        gdatepaie.valeur2 = GetFormaDate(paiesal.datepaiement);
                    }
                    if (paiesal.mois == "3")
                    {
                        gpaie.valeur3 = GetForma(paiesal.valeur);
                        if (GetForma(paiesal.valeur) != "0.00")
                            Application.Current.Resources["imoispaye"] = 3;
                        gdatepaie.valeur3 = GetFormaDate(paiesal.datepaiement);
                    }
                    if (paiesal.mois == "4")
                    {
                        gpaie.valeur4 = GetForma(paiesal.valeur);
                        if (GetForma(paiesal.valeur) != "0.00")
                            Application.Current.Resources["imoispaye"] = 4;
                        gdatepaie.valeur4 = GetFormaDate(paiesal.datepaiement);
                    }
                    if (paiesal.mois == "5")
                    {
                        gpaie.valeur5 = GetForma(paiesal.valeur);
                        if (GetForma(paiesal.valeur) != "0.00")
                            Application.Current.Resources["imoispaye"] = 5;
                        gdatepaie.valeur5 = GetFormaDate(paiesal.datepaiement);
                    }
                    if (paiesal.mois == "6")
                    {
                        gpaie.valeur6 = GetForma(paiesal.valeur);
                        if (GetForma(paiesal.valeur) != "0.00")
                            Application.Current.Resources["imoispaye"] = 6;
                        gdatepaie.valeur6 = GetFormaDate(paiesal.datepaiement);
                    }
                    if (paiesal.mois == "7")
                    {
                        gpaie.valeur7 = GetForma(paiesal.valeur);
                        if (GetForma(paiesal.valeur) != "0.00")
                            Application.Current.Resources["imoispaye"] = 7;
                        gdatepaie.valeur7 = GetFormaDate(paiesal.datepaiement);
                    }
                    if (paiesal.mois == "8")
                    {
                        gpaie.valeur8 = GetForma(paiesal.valeur);
                        if (GetForma(paiesal.valeur) != "0.00")
                            Application.Current.Resources["imoispaye"] = 8;
                        gdatepaie.valeur8 = GetFormaDate(paiesal.datepaiement);
                    }
                    if (paiesal.mois == "9")
                    {
                        gpaie.valeur9 = GetForma(paiesal.valeur);
                        if (GetForma(paiesal.valeur) != "0.00")
                            Application.Current.Resources["imoispaye"] = 9;
                        gdatepaie.valeur9 = GetFormaDate(paiesal.datepaiement);
                    }
                    if (paiesal.mois == "10")
                    {
                        gpaie.valeur10 = GetForma(paiesal.valeur);
                        if (GetForma(paiesal.valeur) != "0.00")
                            Application.Current.Resources["imoispaye"] = 10;
                        gdatepaie.valeur10 = GetFormaDate(paiesal.datepaiement);
                    }
                    if (paiesal.mois == "11")
                    {
                        gpaie.valeur11 = GetForma(paiesal.valeur);
                        if (GetForma(paiesal.valeur) != "0.00")
                            Application.Current.Resources["imoispaye"] = 11;
                        gdatepaie.valeur11 = GetFormaDate(paiesal.datepaiement);
                    }
                    if (paiesal.mois == "12")
                    {
                        gpaie.valeur12 = GetForma(paiesal.valeur);
                        if (GetForma(paiesal.valeur) != "0.00")
                            Application.Current.Resources["imoispaye"] = 12;
                        gdatepaie.valeur12 = GetFormaDate(paiesal.datepaiement);
                    }
                    if (paiesal.mois == "13")
                    {
                        gpaie.treisieme = gpaie.valeur13 = GetForma(paiesal.valeur);
                        if (GetForma(paiesal.valeur) != "0.00")
                            Application.Current.Resources["imoispaye"] = 13;
                        gdatepaie.treisieme = gdatepaie.valeur13 = GetFormaDate(paiesal.datepaiement);
                    }


                }
                gpaie.totannee = GetForma(totan);
                gdatepaie.totannee = "";
                gpaie.totalgeneral = GetForma(totan + UT.ToDecimal(gpaie.treisieme));
                gdatepaie.totalgeneral = "";
                UT.Grillesalaries.Add(gpaie);
                UT.Grillesalaries.Add(gdatepaie);

            }
            gv_grillesalaire.ItemsSource = UT.Grillesalaries; //.Where(w => UT.ToDecimal(w.taux) > 0 || w.deduction == "0" || w.deduction == "+") ;
            if (gv_grillesalaire.HasItems)
            {
                if (bgrillev)
                {
                    this.Resources["bGrilleValide"] = true;
                    this.Resources["_bGrilleValide"] = false;
                }
                else
                {
                    this.Resources["bGrilleValide"] = false;
                    this.Resources["_bGrilleValide"] = true;
                }
            }
        }

        private void bt_grille_valider_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult rep = System.Windows.Forms.MessageBox.Show("Etes-vous sûre de valider la grille ?", "Grille de salaire", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (rep == System.Windows.Forms.DialogResult.Yes)
            {

                UT.enregistrerGrillesalarie(gv_grillesalaire.Items[0] as utilitaire.Grillesalarie, 3, true);
                foreach (utilitaire.Grillesalarie gs in gv_grillesalaire.Items) 
                {
                    if (gs.rang == null || gs.rang == "0")
                        continue;
                    //gs.valeur1 = gs.valeur2 = gs.valeur3 = gs.valeur4 = gs.valeur5 = gs.valeur6 = gs.valeur7 = gs.valeur8 = gs.valeur9 = gs.valeur10 = gs.valeur11 = gs.valeur12 = gs.valeur;
                    //gs.valeur13 = gs.treisieme;
                    
                    string f = gs.valeur2;
                    //string sbaseinst = (gv_grillesalaire.Items  .Where(w => w.idsalarie == gs.idsalarie && w.code)
                    UT.enregistrerGrillesalarie(gs, 1);
                }
            }
            else
                return;
            System.Windows.Forms.MessageBox.Show("Validation éfféctuée avec succès ! ", "Grille de salaire", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            chargergrille();
            //UT.GrillesalarieTmps.Clear();
            //UT.chargerGrillesalarie(cmb_Entreprise.SelectedValue.ToString(), ed_tx_annee.Text.Trim(), 3); //Toutes les grilles
        }

        private void bt_grille_reinitialiser_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult rep = System.Windows.Forms.MessageBox.Show("Etes-vous sûre de réinitialiser la grille ?", "Grille de salaire", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (rep == System.Windows.Forms.DialogResult.Yes)
            {

                UT.enregistrerGrillesalarie(gv_grillesalaire.Items[0] as utilitaire.Grillesalarie, 3, true);
                chargergrille();
            }
        }


        string[] listemois = new string[] { "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre", "Treizième", "Treizième" };
        private void gv_grillesalaire_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //string d = (gv_grillesalaire.SelectedItem as utilitaire.Grillesalarie).code;
            //p_paiement.DataContext = null;
            utilitaire.Grillesalarie gs = (gv_grillesalaire.CurrentCell.Item as utilitaire.Grillesalarie);
            this.Resources["bMoisValide"] = false;
            this.Resources["bMoisPaye"] = false;
            if (gs == null)
                return;
            string[] h = listemois;
            ed_grille_mttmodifval.DataContext = gs;
            ck_toutmodifierapartir.IsChecked = false;
            if (h.Contains(gv_grillesalaire.SelectedCells[0].Column.Header) && this.Resources["bGrilleValide"].Equals(true))
            {
                //if (this.Resources["bMoisValide"].Equals(true))
                if (valeurSalairemoisPaye(numMois(gv_grillesalaire.SelectedCells[0].Column.Header.ToString())) == 0)
                    this.Resources["bMoisPaye"] = true;
                else
                    this.Resources["bMoisValide"] = true;


            }
            if (this.Resources["bMoisPaye"].Equals(true) && h.Contains(gv_grillesalaire.SelectedCells[0].Column.Header) && gs.code != "TOTDED" && gs.code != "SALNET" && gs.code != "PAYE" && gs.code != "DATEPAIE" && bt_grille_valider.IsEnabled == false)
            {
                //gv_grillesalaire.SelectedCells[0].Column.ClipboardContentBinding.ToString();
                ed_grille_mttmodifval.IsEnabled = ck_toutmodifierapartir.IsEnabled = bt_grille_modifvalider.IsEnabled = true;
                //var be = BindingOperations.GetBindingExpression(ed_grille_mttmodifval, TextBox.TextProperty).ParentBinding;
                Binding be = new Binding();
                //BindingBase d = null;
                //ed_grille_mttmodifval.SetBinding(TextBox.TextProperty, d);

                be.UpdateSourceTrigger = UpdateSourceTrigger.Explicit;
                be.Mode = BindingMode.TwoWay;
                switch (gv_grillesalaire.SelectedCells[0].Column.Header.ToString())
                {
                    case "Janvier":
                        be.Path = new PropertyPath( "valeur1");
                        ed_grille_mttmodifval.SetBinding(TextBox.TextProperty, be);
                        //ed_grille_mttmodifval.Text = gs.valeur1;
                        break;
                    case "Février":
                        be.Path = new PropertyPath( "valeur2");
                        ed_grille_mttmodifval.SetBinding(TextBox.TextProperty, be);
                        //ed_grille_mttmodifval.Text = gs.valeur2;
                        break;
                    case "Mars":
                        be.Path = new PropertyPath( "valeur3");
                        ed_grille_mttmodifval.SetBinding(TextBox.TextProperty, be);
                        //ed_grille_mttmodifval.Text = gs.valeur3;
                        break;
                    case "Avril":
                        //ed_grille_mttmodifval.Text = gs.valeur4;
                        be.Path = new PropertyPath( "valeur4");
                        ed_grille_mttmodifval.SetBinding(TextBox.TextProperty, be);
                        break;
                    case "Mai":
                        be.Path = new PropertyPath( "valeur5");
                        ed_grille_mttmodifval.SetBinding(TextBox.TextProperty, be);
                        //ed_grille_mttmodifval.Text = gs.valeur5;
                        break;
                    case "Juin":
                        be.Path = new PropertyPath( "valeur6");
                        ed_grille_mttmodifval.SetBinding(TextBox.TextProperty, be);
                        //ed_grille_mttmodifval.Text = gs.valeur6;
                        break;
                    case "Juillet":
                        be.Path = new PropertyPath( "valeur7");
                        ed_grille_mttmodifval.SetBinding(TextBox.TextProperty, be);
                        //ed_grille_mttmodifval.Text = gs.valeur7;
                        break;
                    case "Août":
                        be.Path = new PropertyPath( "valeur8");
                        ed_grille_mttmodifval.SetBinding(TextBox.TextProperty, be);
                        //ed_grille_mttmodifval.Text = gs.valeur8;
                        break;
                    case "Septembre":
                        be.Path = new PropertyPath( "valeur9");
                        ed_grille_mttmodifval.SetBinding(TextBox.TextProperty, be);
                        //ed_grille_mttmodifval.Text = gs.valeur9;
                        break;
                    case "Octobre":
                        be.Path = new PropertyPath( "valeur10");
                        ed_grille_mttmodifval.SetBinding(TextBox.TextProperty, be);
                        //ed_grille_mttmodifval.Text = gs.valeur10;
                        break;
                    case "Novembre":
                        be.Path = new PropertyPath( "valeur11");
                        ed_grille_mttmodifval.SetBinding(TextBox.TextProperty, be);
                        //ed_grille_mttmodifval.Text = gs.valeur11;
                        break;
                    case "Décembre":
                        be.Path = new PropertyPath( "valeur12");
                        ed_grille_mttmodifval.SetBinding(TextBox.TextProperty, be);
                        //ed_grille_mttmodifval.Text = gs.valeur12;
                        break;
                    case "Treizième":
                        be.Path = new PropertyPath( "valeur13");
                        ed_grille_mttmodifval.SetBinding(TextBox.TextProperty, be);
                        //ed_grille_mttmodifval.Text = gs.valeur13;
                        break;
                   
                }
                
                /*for (int i = 0; i < h.Length; i++)
                { 
                    if (h[i] == gv_grillesalaire.SelectedCells[0].Column.Header)
                        ed_grille_mttmodifval.Text = */
            }
            else
            {
                ed_grille_mttmodifval.Text = "0.00";
                ed_grille_mttmodifval.IsEnabled = ck_toutmodifierapartir.IsEnabled = bt_grille_modifvalider.IsEnabled = false;
            }
        }

        private void modifToutApartir(utilitaire.Grillesalarie gsal, string val, string numM)
        {
            if (numM == "1")
                gsal.valeur2 = gsal.valeur3 = gsal.valeur4 = gsal.valeur5 = gsal.valeur6 = gsal.valeur7 = gsal.valeur8 = gsal.valeur9 = gsal.valeur10 = gsal.valeur11 = gsal.valeur12 = val;
            else if (numM == "2")
                gsal.valeur3 = gsal.valeur4 = gsal.valeur5 = gsal.valeur6 = gsal.valeur7 = gsal.valeur8 = gsal.valeur9 = gsal.valeur10 = gsal.valeur11 = gsal.valeur12 = val;
            else if (numM == "3")
                gsal.valeur4 = gsal.valeur5 = gsal.valeur6 = gsal.valeur7 = gsal.valeur8 = gsal.valeur9 = gsal.valeur10 = gsal.valeur11 = gsal.valeur12 = val;
            else if (numM == "4")
                gsal.valeur5 = gsal.valeur6 = gsal.valeur7 = gsal.valeur8 = gsal.valeur9 = gsal.valeur10 = gsal.valeur11 = gsal.valeur12 = val;
            else if (numM == "5")
                gsal.valeur6 = gsal.valeur7 = gsal.valeur8 = gsal.valeur9 = gsal.valeur10 = gsal.valeur11 = gsal.valeur12 = val;
            else if (numM == "6")
                gsal.valeur7 = gsal.valeur8 = gsal.valeur9 = gsal.valeur10 = gsal.valeur11 = gsal.valeur12 = val;
            else if (numM == "7")
                gsal.valeur8 = gsal.valeur9 = gsal.valeur10 = gsal.valeur11 = gsal.valeur12 = val;
            else if (numM == "8")
                gsal.valeur9 = gsal.valeur10 = gsal.valeur11 = gsal.valeur12 = val;
            else if (numM == "9")
                gsal.valeur10 = gsal.valeur11 = gsal.valeur12 = val;
            else if (numM == "10")
                gsal.valeur11 = gsal.valeur12 = val;
            else if (numM == "11")
                gsal.valeur12 = val;

            if (UT.ToDecimal(gsal.valeur13) > 0)
                gsal.valeur13 = val;
            UT.enregistrerGrillesalarie(gsal, 2);
        }

        private void modifierVal()
        {
            if (ed_grille_mttmodifval.Text.Trim() == "")
                ed_grille_mttmodifval.Text = "0.00";
            string codeLigne = (gv_grillesalaire.SelectedCells[0].Item as utilitaire.Grillesalarie).code;
            
                BindingExpression b = ed_grille_mttmodifval.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty);
                ed_grille_mttmodifval.Text = GetFormaRound(UT.ToDecimal(ed_grille_mttmodifval.Text));
                b.UpdateSource();
                UT.enregistrerGrillesalarie(ed_grille_mttmodifval.DataContext as utilitaire.Grillesalarie, 2);
            if (ck_toutmodifierapartir.IsChecked == true)
                modifToutApartir((gv_grillesalaire.SelectedCells[0].Item as utilitaire.Grillesalarie), ed_grille_mttmodifval.Text, numMois(gv_grillesalaire.SelectedCells[0].Column.Header.ToString()));

            /*ed_grille_mttmodifval.Text = "0.00";
            ed_grille_mttmodifval.IsEnabled = false;*/

            if (codeLigne != "SALMENS")
            {
                chargergrille();
                return;
            }

            utilitaire.Grillesalarie gs = (gv_grillesalaire.SelectedCells[0].Item as utilitaire.Grillesalarie);
            string val = GetFormaRound(UT.ToDecimal(ed_grille_mttmodifval.Text));
            string ancieneval = "0.00";
            object col;
            decimal salm = 0;
            switch (gv_grillesalaire.SelectedCells[0].Column.Header.ToString())
            {
                case "Janvier":
                    ancieneval = gs.valeur1;
                    //ed_grille_mttmodifval.SetBinding(TextBox.TextProperty, "valeur1");
                    salm = UT.ToDecimal(UT.Grillesalaries.Where(w => w.code == "SALMENS").First().valeur1);
                    foreach (utilitaire.Grillesalarie modgs in UT.Grillesalaries.Where(w=> w.taux != null && UT.ToDecimal(w.taux) > 0))
                    {
                        modgs.valeur1 = GetFormaRound((salm * UT.ToDecimal(modgs.taux) / 100).ToString());
                        if (ck_toutmodifierapartir.IsChecked == true)
                        {
                            modgs.valeur2 = modgs.valeur3 = modgs.valeur4 = modgs.valeur5 = modgs.valeur6 = modgs.valeur7 = modgs.valeur8 = modgs.valeur9 = modgs.valeur10 = modgs.valeur11 = modgs.valeur12 = modgs.valeur1;
                            if (UT.ToDecimal(modgs.valeur13) > 0)
                                modgs.valeur13 = modgs.valeur1;
                        }
                        UT.enregistrerGrillesalarie(modgs, 2);
                    }
                    //gv_grillesalaire.Items.MoveCurrentToNext();
                    //gv_grillesalaire.SelectedCells[0] = gv_grillesalaire.Items.MoveCurrentToNext(); UT.Grillesalaries.Where(w => w.code != "TOTDED").First().valeur1;
                    //BindingExpression bb = gv_grillesalaire.GetBindingExpression(DataGridCell.DataContextProperty);
                    //bb.UpdateSource();
                    //gs.valeur1 = val;
                    break;
                case "Février":
                    salm = UT.ToDecimal(UT.Grillesalaries.Where(w => w.code == "SALMENS").First().valeur2);
                    foreach (utilitaire.Grillesalarie modgs in UT.Grillesalaries.Where(w => w.taux != null && UT.ToDecimal(w.taux) > 0))
                    {
                        modgs.valeur2 = GetFormaRound((salm * UT.ToDecimal(modgs.taux) / 100).ToString());
                        if (ck_toutmodifierapartir.IsChecked == true)
                        {
                            modgs.valeur3 = modgs.valeur4 = modgs.valeur5 = modgs.valeur6 = modgs.valeur7 = modgs.valeur8 = modgs.valeur9 = modgs.valeur10 = modgs.valeur11 = modgs.valeur12 = modgs.valeur2;
                            if (UT.ToDecimal(modgs.valeur13) > 0)
                                modgs.valeur13 = modgs.valeur2;
                        }
                        UT.enregistrerGrillesalarie(modgs, 2);
                    }
                    break;
                case "Mars":
                    salm = UT.ToDecimal(UT.Grillesalaries.Where(w => w.code == "SALMENS").First().valeur3);
                    foreach (utilitaire.Grillesalarie modgs in UT.Grillesalaries.Where(w => w.taux != null && UT.ToDecimal(w.taux) > 0))
                    {
                        modgs.valeur3 = GetFormaRound((salm * UT.ToDecimal(modgs.taux) / 100).ToString());
                        if (ck_toutmodifierapartir.IsChecked == true)
                        {
                            modgs.valeur4 = modgs.valeur5 = modgs.valeur6 = modgs.valeur7 = modgs.valeur8 = modgs.valeur9 = modgs.valeur10 = modgs.valeur11 = modgs.valeur12 = modgs.valeur3;
                            if (UT.ToDecimal(modgs.valeur13) > 0)
                                modgs.valeur13 = modgs.valeur3;
                        }
                        UT.enregistrerGrillesalarie(modgs, 2);
                    }
                    break;
                case "Avril":
                    salm = UT.ToDecimal(UT.Grillesalaries.Where(w => w.code == "SALMENS").First().valeur4);
                    foreach (utilitaire.Grillesalarie modgs in UT.Grillesalaries.Where(w => w.taux != null && UT.ToDecimal(w.taux) > 0))
                    {
                        modgs.valeur4 = GetFormaRound((salm * UT.ToDecimal(modgs.taux) / 100).ToString());
                        if (ck_toutmodifierapartir.IsChecked == true)
                        {
                            modgs.valeur5 = modgs.valeur6 = modgs.valeur7 = modgs.valeur8 = modgs.valeur9 = modgs.valeur10 = modgs.valeur11 = modgs.valeur12 = modgs.valeur4;
                            if (UT.ToDecimal(modgs.valeur13) > 0)
                                modgs.valeur13 = modgs.valeur4;
                        }
                        UT.enregistrerGrillesalarie(modgs, 2);
                    }
                    break;
                case "Mai":
                    salm = UT.ToDecimal(UT.Grillesalaries.Where(w => w.code == "SALMENS").First().valeur5);
                    foreach (utilitaire.Grillesalarie modgs in UT.Grillesalaries.Where(w => w.taux != null && UT.ToDecimal(w.taux) > 0))
                    {
                        modgs.valeur5 = GetFormaRound((salm * UT.ToDecimal(modgs.taux) / 100).ToString());
                        if (ck_toutmodifierapartir.IsChecked == true)
                        {
                            modgs.valeur6 = modgs.valeur7 = modgs.valeur8 = modgs.valeur9 = modgs.valeur10 = modgs.valeur11 = modgs.valeur12 = modgs.valeur5;
                            if (UT.ToDecimal(modgs.valeur13) > 0)
                                modgs.valeur13 = modgs.valeur5;
                        }
                        UT.enregistrerGrillesalarie(modgs, 2);
                    }
                    break;
                case "Juin":
                    salm = UT.ToDecimal(UT.Grillesalaries.Where(w => w.code == "SALMENS").First().valeur6);
                    foreach (utilitaire.Grillesalarie modgs in UT.Grillesalaries.Where(w => w.taux != null && UT.ToDecimal(w.taux) > 0))
                    {
                        modgs.valeur6 = GetFormaRound((salm * UT.ToDecimal(modgs.taux) / 100).ToString());
                        if (ck_toutmodifierapartir.IsChecked == true)
                        {
                            modgs.valeur7 = modgs.valeur8 = modgs.valeur9 = modgs.valeur10 = modgs.valeur11 = modgs.valeur12 = modgs.valeur6;
                            if (UT.ToDecimal(modgs.valeur13) > 0)
                                modgs.valeur13 = modgs.valeur6;
                        }
                        UT.enregistrerGrillesalarie(modgs, 2);
                    }
                    break;
                case "Juillet":
                    salm = UT.ToDecimal(UT.Grillesalaries.Where(w => w.code == "SALMENS").First().valeur7);
                    foreach (utilitaire.Grillesalarie modgs in UT.Grillesalaries.Where(w => w.taux != null && UT.ToDecimal(w.taux) > 0))
                    {
                        modgs.valeur7 = GetFormaRound((salm * UT.ToDecimal(modgs.taux) / 100).ToString());
                        if (ck_toutmodifierapartir.IsChecked == true)
                        {
                            modgs.valeur8 = modgs.valeur9 = modgs.valeur10 = modgs.valeur11 = modgs.valeur12 = modgs.valeur7;
                            if (UT.ToDecimal(modgs.valeur13) > 0)
                                modgs.valeur13 = modgs.valeur7;
                        }
                        UT.enregistrerGrillesalarie(modgs, 2);
                    }
                    break;
                case "Août":
                    salm = UT.ToDecimal(UT.Grillesalaries.Where(w => w.code == "SALMENS").First().valeur8);
                    foreach (utilitaire.Grillesalarie modgs in UT.Grillesalaries.Where(w => w.taux != null && UT.ToDecimal(w.taux) > 0))
                    {
                        modgs.valeur8 = GetFormaRound((salm * UT.ToDecimal(modgs.taux) / 100).ToString());
                        if (ck_toutmodifierapartir.IsChecked == true)
                        {
                            modgs.valeur9 = modgs.valeur10 = modgs.valeur11 = modgs.valeur12 = modgs.valeur8;
                            if (UT.ToDecimal(modgs.valeur13) > 0)
                                modgs.valeur13 = modgs.valeur8;
                        }
                        UT.enregistrerGrillesalarie(modgs, 2);
                    }
                    break;
                case "Septembre":
                    salm = UT.ToDecimal(UT.Grillesalaries.Where(w => w.code == "SALMENS").First().valeur9);
                    foreach (utilitaire.Grillesalarie modgs in UT.Grillesalaries.Where(w => w.taux != null && UT.ToDecimal(w.taux) > 0))
                    {
                        modgs.valeur9 = GetFormaRound((salm * UT.ToDecimal(modgs.taux) / 100).ToString());
                        if (ck_toutmodifierapartir.IsChecked == true)
                        {
                            modgs.valeur10 = modgs.valeur11 = modgs.valeur12 = modgs.valeur9;
                            if (UT.ToDecimal(modgs.valeur13) > 0)
                                modgs.valeur13 = modgs.valeur9;
                        }
                        UT.enregistrerGrillesalarie(modgs, 2);
                    }
                    break;
                case "Octobre":
                    salm = UT.ToDecimal(UT.Grillesalaries.Where(w => w.code == "SALMENS").First().valeur10);
                    foreach (utilitaire.Grillesalarie modgs in UT.Grillesalaries.Where(w => w.taux != null && UT.ToDecimal(w.taux) > 0))
                    {
                        modgs.valeur10 = GetFormaRound((salm * UT.ToDecimal(modgs.taux) / 100).ToString());
                        if (ck_toutmodifierapartir.IsChecked == true)
                        {
                            modgs.valeur11 = modgs.valeur12 = modgs.valeur10;
                            if (UT.ToDecimal(modgs.valeur13) > 0)
                                modgs.valeur13 = modgs.valeur10;
                        }
                        UT.enregistrerGrillesalarie(modgs, 2);
                    }
                    break;
                case "Novembre":
                    salm = UT.ToDecimal(UT.Grillesalaries.Where(w => w.code == "SALMENS").First().valeur11);
                    foreach (utilitaire.Grillesalarie modgs in UT.Grillesalaries.Where(w => w.taux != null && UT.ToDecimal(w.taux) > 0))
                    {
                        modgs.valeur11 = GetFormaRound((salm * UT.ToDecimal(modgs.taux) / 100).ToString());
                        if (ck_toutmodifierapartir.IsChecked == true)
                        {
                            modgs.valeur12 = modgs.valeur11;
                            if (UT.ToDecimal(modgs.valeur13) > 0)
                                modgs.valeur13 = modgs.valeur11;
                        }
                        UT.enregistrerGrillesalarie(modgs, 2);
                    }
                    break;
                case "Décembre":
                    salm = UT.ToDecimal(UT.Grillesalaries.Where(w => w.code == "SALMENS").First().valeur12);
                    foreach (utilitaire.Grillesalarie modgs in UT.Grillesalaries.Where(w => w.taux != null && UT.ToDecimal(w.taux) > 0))
                    {
                        modgs.valeur12 = GetFormaRound((salm * UT.ToDecimal(modgs.taux) / 100).ToString());
                        if (ck_toutmodifierapartir.IsChecked == true)
                        {
                            if (UT.ToDecimal(modgs.valeur13) > 0)
                                modgs.valeur13 = modgs.treisieme = modgs.valeur12;
                        }
                        UT.enregistrerGrillesalarie(modgs, 2);
                    }
                    break;
                case "Treizième":
                    salm = UT.ToDecimal(UT.Grillesalaries.Where(w => w.code == "SALMENS").First().valeur13);
                    foreach (utilitaire.Grillesalarie modgs in UT.Grillesalaries.Where(w => w.taux != null && UT.ToDecimal(w.taux) > 0))
                    {
                        modgs.valeur13 = modgs.treisieme = GetFormaRound((salm * UT.ToDecimal(modgs.taux) / 100).ToString());
                        UT.enregistrerGrillesalarie(modgs, 2);
                    }
                    break;

            }
            chargergrille();
            /*decimal dancieneval = UT.ToDecimal(ancieneval);
            decimal dnouveauval = UT.ToDecimal(val);
            if (dnouveauval != dancieneval) //il y a eu modif : calcul somme deduc et net
            {
                decimal stot = UT.Grillesalaries.Where(w => w.code != "SALMENS" && w.code != "TOTDED" && w.code != "SALNET").Sum(w => decimal.Parse(Getdecimal(w.valeur)));
            }*/
            //gv_grillesalaire.SelectedCells[0].Item = val;
        }

        private void ed_grille_modifval_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                modifierVal();
        }

        public class CompareStrings : IComparer<string>
        {
            // Because the class implements IComparer, it must define a 
            // Compare method. The method returns a signed integer that indicates 
            // whether s1 > s2 (return is greater than 0), s1 < s2 (return is negative),
            // or s1 equals s2 (return value is 0). This Compare method compares strings. 
            public int Compare(string s1, string s2)
            {
                return string.Compare(s1, s2, true);
            }
        }
        public class CompareIntegers : IComparer<int>
        {
            // Because the class implements IComparer, it must define a 
            // Compare method. This Compare method compares integers.
            public int Compare(int i1, int i2)
            {
                return i1 - i2;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            if (gv_grillesalaire.SelectedCells.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Veuillez sélectionner une colonne à imprimer ! ", "Fiche de paie", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

                return;
            }
            f_etats ff = new DeltaSalaire.f_etats();
            string ssel = "";

            for (int i=0; i< gv_grillesalaire.SelectedCells.Count; i++)
            {
                string[] f = ssel.Split(',');
                int colind = gv_grillesalaire.SelectedCells[i].Column.DisplayIndex;
                if (colind - 3 > 13)
                    colind = colind - 4;
                else
                    colind = colind - 3;
                if (f.Contains((colind).ToString()))
                    continue;
                if (ssel == "")
                    ssel += (colind).ToString();
                else
                    ssel += "," + (colind).ToString();
                
            }
            string[] sselTri = ssel.Split(',');
            sselTri = sselTri.OrderBy(a => int.Parse(a), new CompareIntegers()).ToArray();
            ff.mois = sselTri;
            ff.idsal = cmb_grille_salarie.SelectedValue.ToString();
            ff.ann = ed_tx_annee.Text.Trim();
            if (sender != bt_grilleImprimer)
                ff.bavantpaiement = true;
            else
                ff.bavantpaiement = false;
            ff.Owner = this;
            ff.ShowDialog();
        }

        private void ed_tx_deduction_TextChanged(object sender, TextChangedEventArgs e)
        {
                ck_tx_deduction.IsChecked = true;
            if (ed_tx_deduction.Text.Trim() == "" || ed_tx_deduction.Text.Trim() == "+")
                ck_tx_deduction.IsChecked = false;
        }

        private void ck_tx_deduction_Click(object sender, RoutedEventArgs e)
        {
            if (ck_tx_deduction.IsChecked == true)
                ed_tx_deduction.Text = "1";
            else
                ed_tx_deduction.Text = "0";

        }

        private void ed_banque_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (bEtat == 0)
            {
                if ((dataGrid.SelectedItem as utilitaire.Salarie).idbanque == null || (dataGrid.SelectedItem as utilitaire.Salarie).idbanque == "")
                    return;
                IEnumerable<utilitaire.Banque> res = UT.banques.Where(w => w.idbanque == (dataGrid.SelectedItem as utilitaire.Salarie).idbanque);
                cmb_banque.ItemsSource = res;
                
                cmb_banque.SelectedValue = (dataGrid.SelectedItem as utilitaire.Salarie).idbanque;
            }
            else if (bEtat > 0)
            {
                if (ed_banque.Text.Length < 3)
                    return;
                IEnumerable<utilitaire.Banque> res = UT.banques.Where(w => w.nocb.Contains(ed_banque.Text.Trim()) || w.nosic.Contains(ed_banque.Text.Trim()) || w.swift.Contains(ed_banque.Text.Trim()) || w.nomabrege.Contains(ed_banque.Text.Trim()));
                cmb_banque.ItemsSource = res;
                
            }
        }

        private void cmb_banque_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (bEtat > 0)
            {
                if (cmb_banque.SelectedItem != null)
                    ed_banque1.Text = cmb_banque.SelectedValue.ToString();
            }
        }

        private void ed_banque1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (bEtat == 0)
            {
                if ((dataGrid.SelectedItem as utilitaire.Salarie).idbanque == null || (dataGrid.SelectedItem as utilitaire.Salarie).idbanque == "")
                    return;
                IEnumerable<utilitaire.Banque> res = UT.banques.Where(w => w.idbanque == (dataGrid.SelectedItem as utilitaire.Salarie).idbanque);
                cmb_banque.ItemsSource = res;
                cmb_banque.SelectedValue = (dataGrid.SelectedItem as utilitaire.Salarie).idbanque;
            }
        }

        private void bt_grillePaiement_Click(object sender, RoutedEventArgs e)
        {
            if (!TesterPaiementMoisPrecedent(gv_grillesalaire.SelectedCells[0].Column.Header.ToString()))
            {
                MessageBox.Show("Les salaires précédents ce mois sélectionné ne sont pas encore payé ! Veuillez faire les paiements par ordre !", "Paiement salaire", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            string nummoisdejavalide = UT.ValeurParCond(UT.dbase + ".paie_paiementsalaire", "idpaiement, envoye, mois", "mois", "envoye=0");
            if (nummoisdejavalide != "" && nummoisdejavalide != numMois(listemois[int.Parse(numMois(gv_grillesalaire.SelectedCells[0].Column.Header.ToString())) - 1]))
            {
                MessageBox.Show("Il y a déjà une liste de paiement d'un mois différent ! Veuillez débord valider celle-ci ou l'annuler !", "Paiement salaire", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            System.Windows.Forms.DialogResult rep = System.Windows.Forms.MessageBox.Show("Etes-vous sûre de valider ce paiement de salaire ?", "Gestion Salaire", System.Windows.Forms.MessageBoxButtons.YesNo);
            

            if (rep == System.Windows.Forms.DialogResult.Yes)
            {
                utilitaire.PaiementSalaire newpaiement = new utilitaire.PaiementSalaire();
                newpaiement.annee = ed_tx_annee.Text;
                newpaiement.datepaiement = (DateTime.Now.Date.ToShortDateString());
                newpaiement.identreprise = (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).identreprise;
                newpaiement.idsalarie = cmb_grille_salarie.SelectedValue.ToString();

                newpaiement.mois = numMois(listemois[int.Parse(numMois(gv_grillesalaire.SelectedCells[0].Column.Header.ToString())) - 1]);
                //newpaiement.mois = listemois[int.Parse(numMois(gv_grillesalaire.SelectedCells[0].Column.Header.ToString())) - 1];
                newpaiement.valeur = UT.ToDecimal(valeurSalairemois(numMois(gv_grillesalaire.SelectedCells[0].Column.Header.ToString())));
                
                //(p_paiement.DataContext as utilitaire.PaiementSalaire).mois = numMois((p_paiement.DataContext as utilitaire.PaiementSalaire).mois); // listemois[int.Parse(numMois(gv_grillesalaire.SelectedCells[0].Column.Header.ToString())) - 1]
                UT.enregistrerPaiement(newpaiement as utilitaire.PaiementSalaire, 1);
                //comptabilier();
                //p_paiement.DataContext = null;
                tb_gessalaire.SelectedIndex = 1;
                //chargergrille();
                op_envoipaiement.IsChecked = true;
                ChargerPaiement(0);
                button_Click(bt_grillePaiement, new RoutedEventArgs());
                //ed_paiement_mois.Text = "";
            }
        }

        private string valeurSalairemois(string nummois)
        {
            string mont = "0.00";
            if (nummois == "1")
                mont = UT.Grillesalaries.Where(w => w.code == "SALNET").First().valeur1;
            else if (nummois == "2")
                mont = UT.Grillesalaries.Where(w => w.code == "SALNET").First().valeur2;
            else if (nummois == "3")
                mont = UT.Grillesalaries.Where(w => w.code == "SALNET").First().valeur3;
            else if (nummois == "4")
                mont = UT.Grillesalaries.Where(w => w.code == "SALNET").First().valeur4;
            else if (nummois == "5")
                mont = UT.Grillesalaries.Where(w => w.code == "SALNET").First().valeur5;
            else if (nummois == "6")
                mont = UT.Grillesalaries.Where(w => w.code == "SALNET").First().valeur6;
            else if (nummois == "7")
                mont = UT.Grillesalaries.Where(w => w.code == "SALNET").First().valeur7;
            else if (nummois == "8")
                mont = UT.Grillesalaries.Where(w => w.code == "SALNET").First().valeur8;
            else if (nummois == "9")
                mont = UT.Grillesalaries.Where(w => w.code == "SALNET").First().valeur9;
            else if (nummois == "10")
                mont = UT.Grillesalaries.Where(w => w.code == "SALNET").First().valeur10;
            else if (nummois == "11")
                mont = UT.Grillesalaries.Where(w => w.code == "SALNET").First().valeur11;
            else if (nummois == "12")
                mont = UT.Grillesalaries.Where(w => w.code == "SALNET").First().valeur12;
            else if (nummois == "13")
                mont = UT.Grillesalaries.Where(w => w.code == "SALNET").First().valeur13;
            return mont;
        }

        private decimal valeurSalairemoisPaye(string nummois)
        {
            string mont = "0.00";
            if (nummois == "1")
                mont = UT.Grillesalaries.Where(w => w.code == "PAYE").First().valeur1;
            else if (nummois == "2")
                mont = UT.Grillesalaries.Where(w => w.code == "PAYE").First().valeur2;
            else if (nummois == "3")
                mont = UT.Grillesalaries.Where(w => w.code == "PAYE").First().valeur3;
            else if (nummois == "4")
                mont = UT.Grillesalaries.Where(w => w.code == "PAYE").First().valeur4;
            else if (nummois == "5")
                mont = UT.Grillesalaries.Where(w => w.code == "PAYE").First().valeur5;
            else if (nummois == "6")
                mont = UT.Grillesalaries.Where(w => w.code == "PAYE").First().valeur6;
            else if (nummois == "7")
                mont = UT.Grillesalaries.Where(w => w.code == "PAYE").First().valeur7;
            else if (nummois == "8")
                mont = UT.Grillesalaries.Where(w => w.code == "PAYE").First().valeur8;
            else if (nummois == "9")
                mont = UT.Grillesalaries.Where(w => w.code == "PAYE").First().valeur9;
            else if (nummois == "10")
                mont = UT.Grillesalaries.Where(w => w.code == "PAYE").First().valeur10;
            else if (nummois == "11")
                mont = UT.Grillesalaries.Where(w => w.code == "PAYE").First().valeur11;
            else if (nummois == "12")
                mont = UT.Grillesalaries.Where(w => w.code == "PAYE").First().valeur12;
            else if (nummois == "13")
                mont = UT.Grillesalaries.Where(w => w.code == "PAYE").First().valeur13;
            return UT.ToDecimal( mont);
        }
        private string numMois(string mois)
        {
            string im = "1";
            for (int i=1; i<= listemois.Length; i++)
            {
                if (mois == listemois[i-1])
                {
                    im = i.ToString();
                    break;
                }
            }
            return im;
        }
        private bool TesterPaiementMoisPrecedent(string moisencours)
        {
            string mm = gv_grillesalaire.SelectedCells[0].Column.Header.ToString();
            int pos = -1;
            for(int i = 0; i < listemois.Length; i++)
            {
                if (listemois[i] == moisencours)
                {
                    pos = i;
                    break;
                }
            }
            if (UT.PaiementSalaires.Where(w => w.mois == (pos).ToString()).Count() != 0 || moisencours == "Janvier")
                return true;
            else
                return false;
        }

        private void bt_paiement_valider_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private string GetvalMois(utilitaire.Grillesalarie gs, string smois)
        {
            string val = "";

            if (smois == "1")
                val = gs.valeur1;
            else if (smois == "2")
                val = gs.valeur2;
            else if (smois == "3")
                val = gs.valeur3;
            else if (smois == "4")
                val = gs.valeur4;
            else if (smois == "5")
                val = gs.valeur5;
            else if (smois == "6")
                val = gs.valeur6;
            else if (smois == "7")
                val = gs.valeur7;
            else if (smois == "8")
                val = gs.valeur8;
            else if (smois == "9")
                val = gs.valeur9;
            else if (smois == "10")
                val = gs.valeur10;
            else if (smois == "11")
                val = gs.valeur11;
            else if (smois == "12")
                val = gs.valeur12;
            else if (smois == "13")
                val = gs.valeur13;
            return val;
        }

        public class CompareStringsSalmens : IComparer<string>
        {
            // Because the class implements IComparer, it must define a 
            // Compare method. The method returns a signed integer that indicates 
            // whether s1 > s2 (return is greater than 0), s1 < s2 (return is negative),
            // or s1 equals s2 (return value is 0). This Compare method compares strings. 
            public int Compare(string s1, string s2 = "SALMENS")
            {
                return string.Compare(s1, "SALMENS", true);
            }
        }

        private void comptabilier()
        {
            string nombasecompta = (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).basecompta;
            int numfolio = int.Parse(UT.Maxsuivant((cmb_Entreprise.SelectedItem as utilitaire.Entreprise).basecompta + ".cpta_mouvement", "numfolio", "year(datemouvement) = " + DateTime.Now.Year.ToString()));
            string idcomptedebit = UT.ValeurParCond((cmb_Entreprise.SelectedItem as utilitaire.Entreprise).basecompta + ".cpta_compte", "idcompte, codecompte", "idcompte", "codecompte = " + (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).comptedebit);
            string idcomptecredit = UT.ValeurParCond((cmb_Entreprise.SelectedItem as utilitaire.Entreprise).basecompta + ".cpta_compte", "idcompte, codecompte", "idcompte", "codecompte = " + (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).comptecredit);
            //string idcomptecharge = UT.ValeurParCond((cmb_Entreprise.SelectedItem as utilitaire.Entreprise).basecompta + ".cpta_compte", "idcompte, codecompte", "idcompte", "codecompte = " + (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).comptecharge);
            //string mm = (p_paiement.DataContext as utilitaire.PaiementSalaire).mois;
            //string ids = (cmb_grille_salarie.SelectedItem as utilitaire.Salarie).idsalaries;
            string[] cd = { "PAYE", "DATEPAIE", "TOTDED", "SALNET" };

            string smois = "0";
            decimal totval = 0;
            decimal totpaye = 0;
            string idsal= "";
            string comptecreditligne;
            string idcomptecreditligne;
            string texteligne;

            foreach (utilitaire.PaiementSalaireEnvoie pm in gv_paiementnvoie.ItemsSource)
            {
                if (idsal != "")
                    idsal += ";";
                
                idsal += pm.idsalarie;
                totpaye += pm.valeur;
            }
            UT.executeSQL(nombasecompta + ".cpta_mouvement", "numfolio,datesaisie,datemouvement,typeecriture,idcompte,codecompte,idcomptec,codecomptec,libelledetail,entree,sortie",
                            numfolio.ToString() + "$" + string.Format("{0:yyyy-MM-dd}", DateTime.Now) + "$" + string.Format("{0:yyyy-MM-dd}", DateTime.Parse(dt_datemouvement.Text)) + "$" + "2" + "$" + idcomptedebit + "$" + (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).comptedebit + "$" + "-1" + "$" + "" + "$" + "Salaire " + lb_paiement_mois.Content.ToString().Replace("mois ", "") + " " + UT.PaiementSalaireEnvoies.First().annee + "$" + "0" + "$" + UT.GetDecimalSql(totpaye.ToString()), 2, "");
            smois = numMois(lb_paiement_mois.Content.ToString());
            //groupement de comptabilisation par mois
            string[] sidsal = idsal.Split(';');
            UT.GrillesalarieTmps.Clear();
            UT.chargerGrillesalarie((cmb_Entreprise.SelectedItem as utilitaire.Entreprise).identreprise, ed_tx_annee.Text.Trim(), 3, int.Parse(smois)); //Toutes les grilles

            IEnumerable<utilitaire.Grillesalarie> res = UT.GrillesalarieTmps.Where(w => sidsal.Contains(w.idsalarie) && !cd.Contains(w.code)).OrderBy(w => w.code);
            string scode = "";
            foreach(utilitaire.Grillesalarie gs in res)
            {
                if (scode != gs.code)
                {
                    string entr = "0";
                    string sort = "0";
                    
                    //if (smois == "1")
                        totval = res.Where(w => w.code == gs.code).Sum(w => UT.ToDecimal(w.valeur));
                    /*else if (smois == "2")
                        totval = res.Where(w => w.code == gs.code).Sum(w => UT.ToDecimal(w.valeur2));
                    else if (smois == "3")
                        totval = res.Where(w => w.code == gs.code).Sum(w => UT.ToDecimal(w.valeur3));
                    else if (smois == "4")
                        totval = res.Where(w => w.code == gs.code).Sum(w => UT.ToDecimal(w.valeur4));
                    else if (smois == "5")
                        totval = res.Where(w => w.code == gs.code).Sum(w => UT.ToDecimal(w.valeur5));
                    else if (smois == "6")
                        totval = res.Where(w => w.code == gs.code).Sum(w => UT.ToDecimal(w.valeur6));
                    else if (smois == "7")
                        totval = res.Where(w => w.code == gs.code).Sum(w => UT.ToDecimal(w.valeur7));
                    else if (smois == "8")
                        totval = res.Where(w => w.code == gs.code).Sum(w => UT.ToDecimal(w.valeur8));
                    else if (smois == "9")
                        totval = res.Where(w => w.code == gs.code).Sum(w => UT.ToDecimal(w.valeur9));
                    else if (smois == "10")
                        totval = res.Where(w => w.code == gs.code).Sum(w => UT.ToDecimal(w.valeur10));
                    else if (smois == "11")
                        totval = res.Where(w => w.code == gs.code).Sum(w => UT.ToDecimal(w.valeur11));
                    else if (smois == "12")
                        totval = res.Where(w => w.code == gs.code).Sum(w => UT.ToDecimal(w.valeur12));
                    else if (smois == "13")
                        totval = res.Where(w => w.code == gs.code).Sum(w => UT.ToDecimal(w.valeur13));*/

                    if (gs.code == "SALMENS")
                    {
                        idcomptecreditligne = idcomptecredit;
                        comptecreditligne = (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).comptecredit;
                        texteligne = "Salaire mensuel";
                        sort = UT.GetDecimalSql(totval.ToString());
                    }
                    else
                    {
                        comptecreditligne = UT.TauxEntreprises.Where(w => w.typeparam == gs.code).First().comptecompta;
                        idcomptecreditligne = UT.ValeurParCond((cmb_Entreprise.SelectedItem as utilitaire.Entreprise).basecompta + ".cpta_compte", "idcompte, codecompte", "idcompte", "codecompte = " + comptecreditligne);
                        if (gs.deduction == "1")
                            entr = UT.GetDecimalSql(totval.ToString());
                        else
                            sort = UT.GetDecimalSql(totval.ToString());
                    }
                    UT.executeSQL(nombasecompta + ".cpta_mouvement", "numfolio,datesaisie,datemouvement,typeecriture,idcompte,codecompte,idcomptec,codecomptec,libelledetail,entree,sortie",
                        numfolio.ToString() + "$" + string.Format("{0:yyyy-MM-dd}", DateTime.Now) + "$" + string.Format("{0:yyyy-MM-dd}", DateTime.Parse(dt_datemouvement.Text)) + "$" + "2" + "$" + idcomptedebit + "$" + (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).comptedebit + "$" + idcomptecreditligne + "$" + comptecreditligne + "$" + gs.code + "$" + entr + "$" + sort, 2, "");


                }
                scode = gs.code;

            }

            totval = 0;
            idsal = "";

            //Charges mensuels
            //numfolio = int.Parse(UT.Maxsuivant((cmb_Entreprise.SelectedItem as utilitaire.Entreprise).basecompta + ".cpta_mouvement", "numfolio", "year(datemouvement) = " + DateTime.Now.Year.ToString()));
            

            //
            UT.executeSQLTexte("UPDATE " + UT.dbase + ".paie_paiementsalaire SET envoye = " + numfolio + " WHERE envoye = 0");
            if (MessageBox.Show(this, "Voulez-vous effectuer la comptabilisation des charges ?", "Paiement salaire", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                CalculChargeMensuel(smois);
            op_paiementenvoye.IsChecked = true; // à envoyer

            /*
            foreach (utilitaire.Grillesalarie gs in gv_grillesalaire.ItemsSource)
            {
                
                if (!cd.Contains(gs.code))
                {
                    if (gs.code == "SALMENS")
                    {
                        idcomptecreditligne = idcomptecredit;
                        comptecreditligne = (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).comptecredit;
                        texteligne = "Salaire mensuel";
                        sort = UT.GetDecimalSql(UT.GetValeurSalaire(ids, ed_tx_annee.Text.Trim(), mm, gs.code));
                    }
                    else
                    {
                        comptecreditligne = UT.TauxEntreprises.Where(w => w.typeparam == gs.code).First().comptecompta;
                        idcomptecreditligne = UT.ValeurParCond((cmb_Entreprise.SelectedItem as utilitaire.Entreprise).basecompta + ".cpta_compte", "idcompte, codecompte", "idcompte", "codecompte = " + comptecreditligne);
                        if (gs.deduction == "1")
                            entr = UT.GetDecimalSql(UT.GetValeurSalaire(ids, ed_tx_annee.Text.Trim(), mm, gs.code));
                        else
                            sort = UT.GetDecimalSql(UT.GetValeurSalaire(ids, ed_tx_annee.Text.Trim(), mm, gs.code));
                    }
                    UT.executeSQL(nombasecompta + ".cpta_mouvement", "numfolio,datesaisie,datemouvement,typeecriture,idcompte,codecompte,idcomptec,codecomptec,libelledetail,entree,sortie",
                        numfolio.ToString() + "$" + string.Format("{0:yyyy-MM-dd}", DateTime.Now) + "$" + string.Format("{0:yyyy-MM-dd}", DateTime.Parse((p_paiement.DataContext as utilitaire.PaiementSalaire).datepaiement)) + "$" + "2" + "$" + idcomptedebit + "$" + (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).comptecredit + "$" + idcomptecreditligne + "$" + comptecreditligne + "$" + gs.txtligne + "$" + entr + "$" + sort, 2, "");
                                
                }
            }
            */
            string sql = "";
        }

        private class chargesmens
        {
            public string typeparam { get; set; }
            public string txtparam { get; set; }
            public decimal montant { get; set; }
            public decimal taux { get; set; }
        }
        private void CalculChargeMensuel(string nmois)
        {
            nmois = int.Parse(nmois).ToString();
            UT.chargerDonneeBaseSalaries("", (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).identreprise);
            string scomptecharge = (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).comptecharge;
            string sbasecompta = (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).basecompta;
            ObservableCollection<chargesmens> cmliste = new ObservableCollection<chargesmens>();
            int numfolio = int.Parse(UT.Maxsuivant(sbasecompta + ".cpta_mouvement", "numfolio", "year(datemouvement) = " + DateTime.Now.Year.ToString()));
            string idcompte = UT.ValeurParCond(sbasecompta + ".cpta_compte", "idcompte, codecompte", "idcompte", "codecompte = " + (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).comptecharge);
            
            decimal dtotgen = 0;
            IEnumerable<IGrouping<string, utilitaire.TauxEntreprise>> tte = UT.TauxEntreprises.Where(x => x.identreprise == (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).identreprise).GroupBy(w => w.typeparam);
            
            foreach (var ste in tte)
            {
                utilitaire.TauxEntreprise te = ste.AsEnumerable().First() as utilitaire.TauxEntreprise;
                chargesmens cm = new chargesmens();
                te = te as utilitaire.TauxEntreprise;
                cm.typeparam = te.typeparam;
                cm.txtparam = te.txtparam;
                decimal dtotval = 0;
                //decimal totbasemasse = UT.GrillesalarieTmps.Where(w => sidsal.Contains(w.idsalarie) && w.code == te.typeparam).Sum(w => UT.ToDecimal(w.basemasse));
                string formbase = te.formulebase;
                decimal dpartempl = 0;
                decimal dpartsala = 0;
                if (formbase == "") //chaque typeparam
                {
                    foreach (utilitaire.DonneeBaseSalarie dbs in UT.DonneeBaseSalarieTmps.Where(x => x.code == te.typeparam))
                    {
                        decimal tauxemp = UT.ToDecimal(dbs.partemployeur);
                        decimal tauxsal = UT.ToDecimal(dbs.partemploye);
                        decimal dsalmens = UT.ToDecimal(GetvalMois(UT.GrillesalarieTmps.Where(y => y.idsalarie == dbs.idsalarie && y.code == "SALMENS").First(), nmois));
                        decimal dbasecalc = UT.ToDecimal(dbs.basecalc) / UT.ToDecimal(dbs.repartition);
                        if (dbasecalc == 0)
                            dbasecalc = dsalmens;
                        

                        dpartsala = UT.ToDecimal(GetvalMois(UT.GrillesalarieTmps.Where(y => y.idsalarie == dbs.idsalarie && y.code == dbs.code).First(), nmois));
                        dpartempl = UT.ToDecimal( GetFormaRound(dbasecalc * tauxemp / 100));
                        dtotval += dpartempl + dpartsala;
                    }
                }
                else if (formbase == "SALANNU") //tous les salariés
                {
                    decimal tauxemp = UT.ToDecimal(te.tauxemployeur);
                    decimal tauxsal = UT.ToDecimal(te.tauxemploye);
                    decimal dsalmens = 0;
                    foreach (utilitaire.Grillesalarie dbs in UT.GrillesalarieTmps.Where(x => x.code == "SALMENS"))
                    {
                        dsalmens += UT.ToDecimal(GetvalMois(dbs, nmois));
                    }
                    dpartsala = UT.ToDecimal(GetFormaRound(dsalmens * tauxsal / 100));
                    dpartempl = UT.ToDecimal(GetFormaRound(dsalmens * tauxemp / 100));
                    dtotval += dpartempl + dpartsala;
                }
                else // if (formbase == "AVS") //avs de tous les salariés
                {
                    decimal tauxemp = UT.ToDecimal(te.tauxemployeur);
                    decimal tauxsal = UT.ToDecimal(te.tauxemploye);
                    decimal dbasecalc = 0;
                    foreach (utilitaire.DonneeBaseSalarie dbs in UT.DonneeBaseSalarieTmps.Where(x => x.code == formbase))
                    {
                        decimal ttauxemp = UT.ToDecimal(dbs.partemployeur);
                        decimal ttauxsal = UT.ToDecimal(dbs.partemploye);
                        decimal dsalmens = UT.ToDecimal(GetvalMois(UT.GrillesalarieTmps.Where(y => y.idsalarie == dbs.idsalarie && y.code == "SALMENS").First(), nmois));
                        decimal tdbasecalc = UT.ToDecimal(dbs.basecalc) / UT.ToDecimal(dbs.repartition);
                        if (tdbasecalc == 0)
                            tdbasecalc = dsalmens;


                        dpartsala += UT.ToDecimal(GetvalMois(UT.GrillesalarieTmps.Where(y => y.idsalarie == dbs.idsalarie && y.code == formbase).First(), nmois)) ;
                        dpartempl += UT.ToDecimal(GetFormaRound(tdbasecalc * ttauxemp / 100));
                        //dtotval += dpartempl + dpartsala;
                    }
                    dtotval = ((dpartsala+ dpartempl) * tauxsal / 100) + ((dpartsala + dpartempl) * tauxemp / 100);
                    
                    /*foreach (utilitaire.Grillesalarie dbs in UT.GrillesalarieTmps.Where(x => x.code == formbase))
                    {
                        dbasecalc += UT.ToDecimal(GetvalMois(dbs, nmois));
                    }
                    dpartsala = UT.ToDecimal(GetFormaRound(dbasecalc * tauxsal / 100));
                    dpartempl = UT.ToDecimal(GetFormaRound(dbasecalc * tauxemp / 100));
                    dtotval += dpartempl + dpartsala;*/
                }
                if (dtotval == 0)
                    continue;
                string scomptec = te.comptecompta;
                string idcomptec = UT.ValeurParCond((cmb_Entreprise.SelectedItem as utilitaire.Entreprise).basecompta + ".cpta_compte", "idcompte, codecompte", "idcompte", "codecompte = " + scomptec);
                dtotgen += dtotval;
                //eciture compta : scomptecharge > scompte
                UT.executeSQL(sbasecompta + ".cpta_mouvement", "numfolio,datesaisie,datemouvement,typeecriture,idcompte,codecompte,idcomptec,codecomptec,libelledetail,entree,sortie",
                            numfolio.ToString() + "$" + string.Format("{0:yyyy-MM-dd}", DateTime.Now) + "$" + string.Format("{0:yyyy-MM-dd}", DateTime.Parse(dt_datemouvement.Text)) + "$" + "2" + "$" + idcompte + "$" + scomptecharge + "$" + idcomptec + "$" + scomptec + "$" + te.typeparam + "$" + "0" + "$" + UT.GetDecimalSql(dtotval.ToString()), 2, "");

            }
            UT.executeSQL(sbasecompta + ".cpta_mouvement", "numfolio,datesaisie,datemouvement,typeecriture,idcompte,codecompte,idcomptec,codecomptec,libelledetail,entree,sortie",
                numfolio.ToString() + "$" + string.Format("{0:yyyy-MM-dd}", DateTime.Now) + "$" + string.Format("{0:yyyy-MM-dd}", DateTime.Parse(dt_datemouvement.Text)) + "$" + "2" + "$" + idcompte + "$" + scomptecharge + "$" + "-1" + "$" + "" + "$" + "Charges " + UT.listemois[int.Parse(nmois)-1] + "$" + "0" + "$" + UT.GetDecimalSql(dtotgen.ToString()), 2, "");
            UT.executeSQLTexte("UPDATE " + UT.dbase + ".paie_paiementsalaire SET charge = " + numfolio + " WHERE mois =" + nmois);
        }

        private void ChargerPaiement(int typeaffiche)
        {
            if (typeaffiche == 0)
            {
                UT.chargerPaiementEnvoie((cmb_Entreprise.SelectedItem as utilitaire.Entreprise).identreprise, 0);
                gv_paiementnvoie.SelectionMode = DataGridSelectionMode.Extended;
            }
            else
            { 
                UT.chargerPaiementEnvoie((cmb_Entreprise.SelectedItem as utilitaire.Entreprise).identreprise, 1);
                gv_paiementnvoie.SelectionMode = DataGridSelectionMode.Single;
            }
            gv_paiementnvoie.ItemsSource = null;
            gv_paiementnvoie.ItemsSource = UT.PaiementSalaireEnvoies;
            lb_totpaiementenvoie.Content = UT.GetForma(UT.PaiementSalaireEnvoies.Sum(w => w.valeur));
            dt_datemouvement.SelectedDate = DateTime.Now;
            if (UT.PaiementSalaireEnvoies.Count() > 0)
            {
                lb_paiement_mois.Content = listemois[int.Parse(UT.PaiementSalaireEnvoies.First().mois) - 1];
            }
            else
                lb_paiement_mois.Content = "-";

        }
        private void gv_donneebasesalarie_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if(e.Column.DisplayIndex == 3)
            {
                string  gg= (e.EditingElement as TextBox).Text.Trim();
                decimal val = 0;
                try
                {
                    val = UT.ToDecimal(gg);
                }
                catch
                {
                    val = 0;
                }
                (e.EditingElement as TextBox).Text = GetForma(val);


            }
        }

        private void bt_grille_modifvalider_Click(object sender, RoutedEventArgs e)
        {
            modifierVal();
        }

        private void cmb_Ent_banque_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (bEtatEnt > 0)
            {
                if (cmb_Ent_banque.SelectedItem != null)
                    ed_Ent_banque1.Text = cmb_Ent_banque.SelectedValue.ToString();
            }
        }

        private void ed_Ent_banque1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (bEtatEnt == 0)
            {
                if ((cmb_Entreprise.SelectedItem as utilitaire.Entreprise).idbanque == null || (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).idbanque == "")
                    return;
                IEnumerable<utilitaire.Banque> res = UT.banques.Where(w => w.idbanque == (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).idbanque);
                cmb_Ent_banque.ItemsSource = res;
                //cmb_Ent_banque.SelectedItem = (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).idbanque;
                cmb_Ent_banque.SelectedValue = int.Parse((cmb_Entreprise.SelectedItem as utilitaire.Entreprise).idbanque);
            }
        }

        private void ed_Ent_banque_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (bEtatEnt == 0)
            {
                if ((cmb_Entreprise.SelectedItem as utilitaire.Entreprise).idbanque == null || (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).idbanque == "")
                    return;
                IEnumerable<utilitaire.Banque> res = UT.banques.Where(w => w.idbanque == (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).idbanque);
                cmb_Ent_banque.ItemsSource = res;
                cmb_Ent_banque.SelectedValue = int.Parse((cmb_Entreprise.SelectedItem as utilitaire.Entreprise).idbanque);
            }
            else if (bEtatEnt > 0)
            {
                if (ed_Ent_banque.Text.Length < 3)
                    return;
                IEnumerable<utilitaire.Banque> res = UT.banques.Where(w => w.nocb.Contains(ed_Ent_banque.Text.Trim()) || w.nosic.Contains(ed_Ent_banque.Text.Trim()) || w.swift.Contains(ed_Ent_banque.Text.Trim()) || w.nomabrege.Contains(ed_Ent_banque.Text.Trim()));
                cmb_Ent_banque.ItemsSource = res;
                //UT.chargerBanque(cond: ed_Ent_banque.Text.Trim(), idb: "");
                //cmb_Ent_banque.ItemsSource = UT.banques;
            }
        }

        private void bt_paiement_envoyer_Click(object sender, RoutedEventArgs e)
        {

            GenererCAMT();
            comptabilier();
        }

        private void GenererCAMT()
        {
            string szPath = "";
            
            string sztxt = "";
            szPath = Environment.CurrentDirectory + @"\Ebanking"; //.Replace("\\", "\\\\");
            try
            {
                if (System.IO.Directory.Exists(szPath) == false)
                {
                    System.Windows.Forms.MessageBox.Show("Veuillez vérifier que le dossier 'Ebanking' soit présent !", "Envoi paiement ebanking", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return;
                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Veuillez vérifier le répertoire de destination Svp !", "Envoi paiement ebanking", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }
            DateTime t;
            string tmp = "";
            int nbr = 0;
            decimal total = 0;
            
            string typedeb = "";
            typedeb = (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).comptedebit;


            XmlDocument doc = new XmlDocument();
            string newnumpaiement = "MSG-" + UT.PaiementSalaireEnvoies.First().mois.PadLeft(2, '0') + string.Format("{0:ddMM}", DateTime.Now);// gvliste2.Rows[0].Cells["lnumgroupe"].FormattedValue.ToString().PadLeft(6, '0');


            doc.Load(Environment.CurrentDirectory + @"\modele_camt.xml");
            (doc.DocumentElement.GetElementsByTagName("MsgId")).Item(0).InnerXml = newnumpaiement;
            (doc.DocumentElement.GetElementsByTagName("CreDtTm")).Item(0).InnerXml = string.Format("{0:yyyy-MM-dd'T'HH:mm:ss}", DateTime.Now);
            int nbrTransaction = gv_paiementnvoie.Items.Count;
            (doc.DocumentElement.GetElementsByTagName("NbOfTxs")).Item(0).InnerXml = nbrTransaction.ToString();

            (doc.DocumentElement.GetElementsByTagName("CtrlSum")).Item(0).InnerXml = UT.ViderP(UT.PaiementSalaireEnvoies.Sum(w => w.valeur));
            (doc.DocumentElement.GetElementsByTagName("ReqdExctnDt")).Item(0).InnerXml = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(1));
            UT.chargerBanque(cond: "", idb: (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).idbanque);
            /*if (typedeb == "1020") //Banque
            {
                //test((((doc.DocumentElement.GetElementsByTagName("DbtrAcct")) as XmlNodeList).Item(0) as XmlElement).GetElementsByTagName("Id").Item(0) as XmlElement).RemoveChild(((((doc.DocumentElement.GetElementsByTagName("DbtrAcct")) as XmlNodeList).Item(0) as XmlElement).GetElementsByTagName("Id").Item(0) as XmlElement).GetElementsByTagName("Othr").Item(0));
                ((doc.DocumentElement.GetElementsByTagName("DbtrAgt") as XmlNodeList).Item(0) as XmlElement).GetElementsByTagName("BIC").Item(0).InnerXml = "RAIFCH22589";
                (((doc.DocumentElement.GetElementsByTagName("DbtrAcct")) as XmlNodeList).Item(0) as XmlElement).GetElementsByTagName("IBAN").Item(0).InnerXml = "CH4480572000014816513";
            }
            else if (typedeb == "1010") //Poste
            {*/
                //test((((doc.DocumentElement.GetElementsByTagName("DbtrAcct")) as XmlNodeList).Item(0) as XmlElement).GetElementsByTagName("Id").Item(0) as XmlElement).RemoveChild(((((doc.DocumentElement.GetElementsByTagName("DbtrAcct")) as XmlNodeList).Item(0) as XmlElement).GetElementsByTagName("Id").Item(0) as XmlElement).GetElementsByTagName("IBAN").Item(0));
                ((doc.DocumentElement.GetElementsByTagName("DbtrAgt") as XmlNodeList).Item(0) as XmlElement).GetElementsByTagName("BIC").Item(0).InnerXml =  UT.banques.First().swift;
                //((((doc.DocumentElement.GetElementsByTagName("DbtrAcct")) as XmlNodeList).Item(0) as XmlElement).GetElementsByTagName("IBAN").Item(0) as XmlElement).RemoveAll();
                //((((doc.DocumentElement.GetElementsByTagName("DbtrAcct")) as XmlNodeList).Item(0) as XmlElement).GetElementsByTagName("Othr").Item(0) as XmlElement).GetElementsByTagName("Id").Item(0).InnerXml = 19-251164-9
                (((doc.DocumentElement.GetElementsByTagName("DbtrAcct")) as XmlNodeList).Item(0) as XmlElement).GetElementsByTagName("IBAN").Item(0).InnerXml = (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).iban;
            //}

            XmlElement paiement;
            XmlDocument docpaiement = new XmlDocument();
            docpaiement.Load(Environment.CurrentDirectory + @"\modele_camt_paiement.xml");
            int i = 0;
            foreach(utilitaire.PaiementSalaireEnvoie pse in gv_paiementnvoie.Items)
            {

                XmlNode pp;
                pp = doc.CreateNode(XmlNodeType.DocumentFragment, "CdtTrfTxInf", "");
                //XmlElement pp = doc.CreateElement("CdtTrfTxInf", null);

                //pp.Attributes.RemoveNamedItem("xmlns");
                pp.InnerXml = docpaiement.ChildNodes[1].InnerXml;

                (pp.ChildNodes.Item(0) as XmlElement).GetElementsByTagName("InstrId").Item(0).InnerXml = "INSTRID-01-" + (i + 1).ToString().PadLeft(3, '0');
                (pp.ChildNodes.Item(0) as XmlElement).GetElementsByTagName("EndToEndId").Item(0).InnerXml = "ENDTOENDID-" + (i + 1).ToString().PadLeft(3, '0');
                (pp.ChildNodes.Item(0) as XmlElement).GetElementsByTagName("InstdAmt").Item(0).InnerXml = UT.ViderP(pse.valeur);

                
                (pp.ChildNodes.Item(0) as XmlElement).RemoveChild((pp.ChildNodes.Item(0) as XmlElement).GetElementsByTagName("PmtTpInf").Item(0));
                ((pp.ChildNodes.Item(0) as XmlElement).GetElementsByTagName("CdtrAcct").Item(0).ChildNodes[0] as XmlElement).GetElementsByTagName("IBAN").Item(0).InnerXml = pse.iban;
                ((pp.ChildNodes.Item(0) as XmlElement).GetElementsByTagName("CdtrAgt").Item(0).ChildNodes[0] as XmlElement).GetElementsByTagName("BIC").Item(0).InnerXml = pse.swift;
                ((pp.ChildNodes.Item(0) as XmlElement).GetElementsByTagName("CdtrAcct").Item(0).ChildNodes[0] as XmlElement).RemoveChild(((pp.ChildNodes.Item(0) as XmlElement).GetElementsByTagName("CdtrAcct").Item(0).ChildNodes[0] as XmlElement).GetElementsByTagName("Othr").Item(0));
                ((pp.ChildNodes.Item(0) as XmlElement).GetElementsByTagName("RmtInf").Item(0).ChildNodes[0] as XmlElement).RemoveChild(((pp.ChildNodes.Item(0) as XmlElement).GetElementsByTagName("RmtInf").Item(0).ChildNodes[0] as XmlElement).GetElementsByTagName("CdtrRefInf").Item(0));
                
                //(((pp.ChildNodes.Item(0) as XmlElement).GetElementsByTagName("RmtInf").Item(0).ChildNodes[0] as XmlElement).GetElementsByTagName("Ustrd").Item(0)).InnerXml = "PAIEMENT DELTAREAL " + string.Format("{0:yyyyMMdd}", DateTime.Now) + gvliste2.Rows[i].Cells["lnumpaiement"].FormattedValue.ToString().PadLeft(6, '0');
                string benef = "";
                if (pse.beneficiaire.Trim() != "")
                {
                    benef += pse.beneficiaire.Trim();
                }

                ((pp.ChildNodes.Item(0) as XmlElement).GetElementsByTagName("Cdtr").Item(0) as XmlElement).GetElementsByTagName("Nm").Item(0).InnerXml = benef.Replace("&", "and");
                string sadr = pse.adresse;
                if (sadr.Trim() == "")
                    sadr = "-";
                (((pp.ChildNodes.Item(0) as XmlElement).GetElementsByTagName("Cdtr").Item(0) as XmlElement).GetElementsByTagName("PstlAdr").Item(0) as XmlElement).GetElementsByTagName("AdrLine").Item(0).InnerXml = sadr;
                (((pp.ChildNodes.Item(0) as XmlElement).GetElementsByTagName("Cdtr").Item(0) as XmlElement).GetElementsByTagName("PstlAdr").Item(0) as XmlElement).GetElementsByTagName("AdrLine").Item(1).InnerXml = pse.npaville;


                ((doc.DocumentElement.GetElementsByTagName("PmtInf")).Item(0) as XmlElement).AppendChild(pp);
                ((doc.DocumentElement.GetElementsByTagName("CdtTrfTxInf")).Item(i) as XmlElement).RemoveAllAttributes();
                //((doc.DocumentElement.GetElementsByTagName("PmtInf")).Item(0) as XmlElement).AppendChild(docpaiement.ChildNodes.Item(0));
                i++;
            }

            try
            {
                doc.Save(szPath + "\\DeltaSalaire_Paiement_CAMT_" + string.Format("{0:yyyyMMdd_HHmmss}", DateTime.Now) + ".xml");
                
                System.Windows.Forms.MessageBox.Show("Génération du fichier d'exportation version CAMT terminée !" + (char)13 + "Le fichier de sortie se trouve dans le dossier 'EBanking'." + (char)13 + "Veuillez faire le paiement sur le site");
                //Util.AfficherInformation("Génération du fichier d'exportation version CAMT terminée !");
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Erreur lors de la génération du fichier !");
                return;
            }

            //gvliste2.Rows.Clear();
            //gvliste1.Rows.Clear();
            //maj champ numgroupe
            try
            {
                if (typedeb == "1020")
                    System.Diagnostics.Process.Start("firefox.exe", "https://ebanking.raiffeisen.ch/entrance/?alwigul001=6855002#/login");
                else
                    System.Diagnostics.Process.Start("firefox.exe", "https://www.postfinance.ch/ap/ba/fp/html/e-finance/home?login");
            }
            catch
            {
                try
                {
                    if (typedeb == "1020")
                        System.Diagnostics.Process.Start("chrome.exe", "https://ebanking.raiffeisen.ch/entrance/?alwigul001=6855002#/login");
                    else
                        System.Diagnostics.Process.Start("chrome.exe", "https://www.postfinance.ch/ap/ba/fp/html/e-finance/home?login");
                }
                catch { }
            }
            //tc_paiement.SelectTab("tabPage5");
            

        }

        private void op_envoipaiement_Checked(object sender, RoutedEventArgs e)
        {
            ChargerPaiement(0);
        }

        private void op_paiementenvoye_Checked(object sender, RoutedEventArgs e)
        {
            ChargerPaiement(1);
        }

        private void gv_paiementnvoie_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (gv_paiementnvoie.ItemsSource != null && gv_paiementnvoie.Items.Count > 0)
            { 
                lb_paiement_mois.Content = listemois[int.Parse((gv_paiementnvoie.CurrentItem as utilitaire.PaiementSalaireEnvoie).mois) - 1];
            }
        }

        private void bt_paiement_annuler_Click(object sender, RoutedEventArgs e)
        {
            
            
            if (op_envoipaiement.IsChecked == true)
            {
                System.Windows.Forms.DialogResult rep = System.Windows.Forms.MessageBox.Show("Etes-vous sûre d'annuler ce paiement ?", "Paiemet Salaire", System.Windows.Forms.MessageBoxButtons.YesNo);
                if (rep == System.Windows.Forms.DialogResult.No)
                    return;
                foreach (utilitaire.PaiementSalaireEnvoie pe in gv_paiementnvoie.SelectedItems)
                {
                    UT.executeSQLTexte("DELETE FROM " + UT.dbase + ".paie_paiementsalaire WHERE envoye = 0 AND idpaiement = " + pe.idpaiement);

                }
                ChargerPaiement(0);
                chargergrille();
            }
            else if (op_paiementenvoye.IsChecked == true)
            {
                System.Windows.Forms.DialogResult rep = System.Windows.Forms.MessageBox.Show("Attention, c'est un paiement déjà comptabilisé." + (char)13 + 
                    "La ligne compta va aussi être annulée!" + (char)13 + "Etes-vous sûre d'annuler ce paiement ?", "Paiemet Salaire déjà comptabilisé", System.Windows.Forms.MessageBoxButtons.YesNo);
                if (rep == System.Windows.Forms.DialogResult.No)
                    return;
                string nombasecompta = (cmb_Entreprise.SelectedItem as utilitaire.Entreprise).basecompta;

                UT.executeSQLTexte("DELETE FROM " + nombasecompta + ".cpta_mouvement WHERE numfolio = " + (gv_paiementnvoie.SelectedItems[0] as utilitaire.PaiementSalaireEnvoie).envoye + 
                    " AND year(datemouvement) = " + DateTime.Parse((gv_paiementnvoie.SelectedItems[0] as utilitaire.PaiementSalaireEnvoie).datepaiement).Year);
                foreach (utilitaire.PaiementSalaireEnvoie pe in gv_paiementnvoie.SelectedItems)
                {
                    UT.executeSQLTexte("DELETE FROM " + UT.dbase + ".paie_paiementsalaire WHERE envoye = " + pe.envoye);

                }
                ChargerPaiement(1);
                chargergrille();
            }
        }

        private void bt_grillesuivant_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cmb_grille_salarie.SelectedIndex++;
            }
            catch { }
        }

        private void bt_grilleprecedent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cmb_grille_salarie.SelectedIndex--;
            }
            catch { }
        }

        private void bt_calculcharge_Click(object sender, RoutedEventArgs e)
        {

            if (gv_paiementnvoie.SelectedItems.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Veuillez sélectionner une ligne dans un groupe de paiement ! ", "Comptabilisation charges", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

                return;
            }
            string imois = (gv_paiementnvoie.SelectedItem as utilitaire.PaiementSalaireEnvoie).mois;
            CalculChargeMensuel(imois.ToString());
            ChargerPaiement(1);
        }

        private void bt_bd_ajoutpercue_Click(object sender, RoutedEventArgs e)
        {
            if (UT.ToDecimal(ed_mtt_basepercue.Text.Trim()) == 0 || UT.ToDecimal(ed_mtt_tauxpercue.Text.Trim()) == 0)
            {
                System.Windows.Forms.MessageBox.Show("Veuillez enter le montant de la base de calcul et le taux à percevoir !", "Ajout ligne à percevoir", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }
            utilitaire.TauxEntrepriseCopy dbn = new DeltaSalaire.utilitaire.TauxEntrepriseCopy();
            dbn.txtparam = (cmb_listecode.SelectedItem as utilitaire.TauxEntreprise).txtparam;
            dbn.typeparam = (cmb_listecode.SelectedItem as utilitaire.TauxEntreprise).typeparam;
            dbn.conditioncalc = (cmb_listecode.SelectedItem as utilitaire.TauxEntreprise).conditioncalc;
            dbn.deduction = "0";
            dbn.selectionne = true;
            dbn.idbasecalcul = (cmb_listecode.SelectedItem as utilitaire.TauxEntreprise).idbasecalcul;
            dbn.rang = (gv_donneebasesalariepercue.Items.Count + 2).ToString();
            dbn.tauxemployeur = "0";
            dbn.formulebase = UT.GetDecimalSql(ed_mtt_basepercue.Text);
            dbn.tauxemploye = UT.GetDecimal3VirglSql(UT.ToDecimal(ed_mtt_tauxpercue.Text).ToString());
            gv_donneebasesalariepercue.Items.Add(dbn);
            cmb_listecode.SelectedItem = null;
        }

        private void ed_mtt_salairemens_TextChanged(object sender, TextChangedEventArgs e)
        {
            if((bool)(this.Resources["bBaseCalc"]) == false)
            {
                decimal dtot = UT.ToDecimal(ed_mtt_salairemens.Text) * 12 + UT.ToDecimal(ed_mtt_treizieme.Text);
                ed_mtt_salannu.Text = UT.GetDecimalSql(dtot.ToString());
            }
        }

        private void ed_mtt_treizieme_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((bool)(this.Resources["bBaseCalc"]) == false)
            {
                decimal dtot = UT.ToDecimal(ed_mtt_salairemens.Text) * 12 + UT.ToDecimal(ed_mtt_treizieme.Text);
                ed_mtt_salannu.Text = UT.GetDecimalSql(dtot.ToString());
            }
        }

        bool modifInst = false;
        private void cmb_npaville_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((sender as ComboBox).Parent as Grid).Name == "p_adresse")
            {
                if (cmb_npaville.SelectedIndex == -1)
                    return;
                modif = true;

                ed_npacity.Text = (cmb_npaville.SelectedItem as utilitaire.City).Zip;
                //if (bEtat > 0)
                    //(g_selsalarie.DataContext as utilitaire.Salarie).idville = (cmb_npaville.SelectedItem as utilitaire.City).IdVille;
                modif = false;
            }
            else if(((sender as ComboBox).Parent as Grid).Name == "p_ent_adresse")
            {
                if (cmb_Ent_npaville.SelectedIndex == -1)
                    return;
                modifEnt = true;

                ed_Ent_npacity.Text = (cmb_Ent_npaville.SelectedItem as utilitaire.City).Zip;
                if (bEtatEnt > 0)
                    (p_ent_adresse.DataContext as utilitaire.Entreprise).idville = (cmb_Ent_npaville.SelectedItem as utilitaire.City).IdVille;
                modifEnt = false;
            }
            else if (((sender as ComboBox).Parent as Grid).Name == "p_param_adresseinst")
            {
                if (cmb_inst_npaville.SelectedIndex == -1)
                {
                    
                    return;
                }
                modifInst = true;

                ed_inst_npacity.Text = (cmb_inst_npaville.SelectedItem as utilitaire.City).Zip;
                if (bEtatInst == 2)
                {
                    (p_param_adresseinst.DataContext as utilitaire.Institution).idville = (cmb_inst_npaville.SelectedItem as utilitaire.City).IdVille;
                    (p_param_adresseinst.DataContext as utilitaire.Institution).npa = (cmb_inst_npaville.SelectedItem as utilitaire.City).Zip;
                    (g_param_institutions.SelectedItem as utilitaire.Institution).npa = (cmb_inst_npaville.SelectedItem as utilitaire.City).Zip;
                    (p_param_adresseinst.DataContext as utilitaire.Institution).ville = (cmb_inst_npaville.SelectedItem as utilitaire.City).CityName;
                    (g_param_institutions.SelectedItem as utilitaire.Institution).ville = (cmb_inst_npaville.SelectedItem as utilitaire.City).CityName;
                }
                if (bEtatInst == 1)
                {
                    (p_param_adresseinst.DataContext as utilitaire.Institution).idville = (cmb_inst_npaville.SelectedItem as utilitaire.City).IdVille;
                    (p_param_adresseinst.DataContext as utilitaire.Institution).npa = (cmb_inst_npaville.SelectedItem as utilitaire.City).Zip;
                    (p_param_adresseinst.DataContext as utilitaire.Institution).ville = (cmb_inst_npaville.SelectedItem as utilitaire.City).CityName;
                }
                modifInst = false;
            }
        }

        
    }
}
