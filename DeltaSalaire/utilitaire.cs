using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Globalization;
using System.Windows.Data;

namespace DeltaSalaire
{

    public class utilitaire
    {
        public MySqlCommand mscom = new MySqlCommand();
        public MySqlCommand mscom_sel = new MySqlCommand();
        MySqlConnection mscon = new MySqlConnection();
        MySqlConnection mscon_sel = new MySqlConnection();
        public ObservableCollection<Salarie> salaries = new ObservableCollection<Salarie>();
        public ObservableCollection<Contrat> contrats = new ObservableCollection<Contrat>();
        public ObservableCollection<Salarie> salaries_sel = new ObservableCollection<Salarie>();
        public ObservableCollection<Entreprise> entreprises = new ObservableCollection<Entreprise>();
        public ObservableCollection<TauxEntreprise> TauxEntreprises = new ObservableCollection<TauxEntreprise>();
        public ObservableCollection<utilitaire.TauxEntrepriseCopy> TauxEntreprisesCopy = new ObservableCollection<utilitaire.TauxEntrepriseCopy>();
        public ObservableCollection<DonneeBaseSalarie> DonneeBaseSalaries = new ObservableCollection<DonneeBaseSalarie>();
        public ObservableCollection<DonneeBaseSalarie> DonneeBaseSalarieTmps = new ObservableCollection<DonneeBaseSalarie>();
        public ObservableCollection<DonneeSalarieResume> DonneeSalarieResumes = new ObservableCollection<DonneeSalarieResume>();
        public ObservableCollection<DonneeSalarieResume> DonneeSalarieResumesTmp = new ObservableCollection<DonneeSalarieResume>();
        public ObservableCollection<Institution> institutions = new ObservableCollection<Institution>();
        public ObservableCollection<Classeage> classeages = new ObservableCollection<Classeage>();
        public ObservableCollection<GenreLPP> GenreLPPs = new ObservableCollection<GenreLPP>();
        public ObservableCollection<Grillesalarie> Grillesalaries = new ObservableCollection<Grillesalarie>();
        public ObservableCollection<Grillesalarie> GrillesalarieTmps = new ObservableCollection<Grillesalarie>();
        public ObservableCollection<PaiementSalaire> PaiementSalaires = new ObservableCollection<PaiementSalaire>();
        public ObservableCollection<PaiementSalaireEnvoie> PaiementSalaireEnvoies = new ObservableCollection<PaiementSalaireEnvoie>();
        public ObservableCollection<Banque> banques = new ObservableCollection<Banque>();
        public ObservableCollection<FichedePaie> FichedePaies = new ObservableCollection<FichedePaie>();
        
        public Salarie _salcopie;


        public class Classeage
        {
            string pardef = "0";
            public string idclasseage { get; set; }
            public string annee { get; set; }
            public string deannee { get; set; }
            public string aannee { get; set; }
            public string agetxt { get; set; }
            public string anneetxt { get; set; }
            public string sexe { get; set; }
            public string partemployeur { get { return pardef; } set { if (value == null || value.Trim() == "") pardef = "0"; pardef = value; } }
            public string partemploye { get { return pardef; } set { if (value == null || value.Trim() == "") pardef = "0"; pardef = value; } }
            public Classeage()
            {
                this.partemployeur = "0.000";
                this.partemploye = "0.000";
            }
        }
        

        public bool enregistrerClasseage(Classeage sal, int typeMaj)
        {

            string sql = "";
            if (typeMaj == 1)
                sql = "INSERT INTO " + dbase + ".paie_calcullppclasseage SET idclasseage = 0, ";
            else if (typeMaj == 2)
                sql = "UPDATE " + dbase + ".paie_calcullppclasseage SET ";
            else if (typeMaj == 3)
                sql = "DELETE FROM " + dbase + ".paie_calcullppclasseage ";

            if (typeMaj <= 2)
            {
                sql += " " +
                    "annee ='" + sal.annee + "', " +
                    "deannee ='" + sal.deannee + "', " +
                    "aannee ='" + sal.aannee + "', " +
                    "sexe ='" + sal.sexe + "', " +
                    "partemployeur =" + GetDecimal3VirglSql( sal.partemployeur) + ", " +
                    "partemploye =" + GetDecimal3VirglSql(sal.partemploye) ;
            }
            sql = sql.Replace("'null'", "NULL").Replace("''", "NULL");
            if (typeMaj >= 2)
                sql += " WHERE idclasseage =" + sal.idclasseage;
            mscom.CommandText = sql;

            int nn = mscom.ExecuteNonQuery();
            if (nn > 0)
                return true;
            else
                return false;
        }

        public void chargerClasseages(string ann)
        {
            classeages.Clear();
            if (ann.Trim() == "")
                return;
            string sql = "SELECT concat(deannee, '-', aannee) as anneetxt, concat(annee-aannee,'-',annee-deannee) as agetxt, classea.* FROM " + dbase + ".paie_calcullppclasseage classea " +
                "WHERE annee = " + ann + " ORDER BY deannee DESC";
            MySqlDataReader mread;
            mscom.Connection = mscon;

            mscom.CommandType = System.Data.CommandType.Text;

            mscom.CommandText = sql;
            mread = mscom.ExecuteReader();
            while (mread.Read())
            {
                classeages.Add(new Classeage()
                {
                    idclasseage = GetValue(mread, "idclasseage"),
                    annee = GetValue(mread, "annee"),
                    deannee = GetValue(mread, "deannee"),
                    aannee = GetValue(mread, "aannee"),
                    agetxt = GetValue(mread, "agetxt"),
                    anneetxt = GetValue(mread, "anneetxt"),
                    sexe = GetValue(mread, "sexe"),
                    partemployeur = GetValue3Virg(mread, "partemployeur"),
                    partemploye = GetValue3Virg(mread, "partemploye")
                });

            }
            mread.Close();
        }

        public class GenreLPP
        {
            string pardef = "0";
            public string idcalcullpp { get; set; }
            public string annee { get; set; }
            public string typeplan { get; set; }
            public string seuilentree { get; set; }
            public string salaireannuelref { get; set; }
            public string deductioncoord { get; set; }
            public string salaireassuremax { get; set; }
            public string salaireassuremin { get; set; }
            public GenreLPP()
            {
                this.seuilentree = "0.000";
                this.salaireannuelref = "0.000";
                this.deductioncoord = "0.000";
                this.salaireassuremax = "0.000";
                this.salaireassuremin = "0.000";
            }
        }


        public bool enregistrerGenreLPP(GenreLPP sal, int typeMaj)
        {

            string sql = "";
            if (typeMaj == 1)
                sql = "INSERT INTO " + dbase + ".paie_calcullpp SET idcalcullpp = 0, ";
            else if (typeMaj == 2)
                sql = "UPDATE " + dbase + ".paie_calcullpp SET ";
            else if (typeMaj == 3)
                sql = "DELETE FROM " + dbase + ".paie_calcullpp ";

            if (typeMaj <= 2)
            {
                sql += " " +
                    "annee ='" + sal.annee + "', " +
                    "typeplan ='" + sal.typeplan + "', " +
                    "seuilentree =" + GetDecimal3VirglSql(sal.seuilentree) + ", " +
                    "salaireannuelref =" + GetDecimal3VirglSql(sal.salaireannuelref) + ", " +
                    "deductioncoord =" + GetDecimal3VirglSql(sal.deductioncoord) + ", " +
                    "salaireassuremax =" + GetDecimal3VirglSql(sal.salaireassuremax) + ", " +
                    "salaireassuremin =" + GetDecimal3VirglSql(sal.salaireassuremin);
            }
            sql = sql.Replace("'null'", "NULL").Replace("''", "NULL");
            if (typeMaj >= 2)
                sql += " WHERE idcalcullpp =" + sal.idcalcullpp;
            mscom.CommandText = sql;

            int nn = mscom.ExecuteNonQuery();
            if (nn > 0)
                return true;
            else
                return false;
        }

        public void chargerGenreLPP(string ann)
        {
            GenreLPPs.Clear();
            if (ann.Trim() == "")
                return;
            string sql = "SELECT calcullpp.* FROM " + dbase + ".paie_calcullpp calcullpp " +
                "WHERE annee = " + ann + " ORDER BY typeplan DESC";
            MySqlDataReader mread;
            mscom.Connection = mscon;

            mscom.CommandType = System.Data.CommandType.Text;

            mscom.CommandText = sql;
            mread = mscom.ExecuteReader();
            while (mread.Read())
            {
                GenreLPPs.Add(new GenreLPP()
                {
                    idcalcullpp = GetValue(mread, "idcalcullpp"),
                    annee = GetValue(mread, "annee"),
                    typeplan = GetValue(mread, "typeplan"),
                    seuilentree = GetValue(mread, "seuilentree"),
                    salaireannuelref = GetValue(mread, "salaireannuelref"),
                    deductioncoord = GetValue(mread, "deductioncoord"),
                    salaireassuremax = GetValue(mread, "salaireassuremax"),
                    salaireassuremin = GetValue(mread, "salaireassuremin")
                });

            }
            mread.Close();
        }
        public class Institution
        {
            public string idinstitution { get; set; }
            public string idlanguage { get; set; }
            public string norefpolice { get; set; }
            public string societe { get; set; }
            public string co { get; set; }
            public string adresse1 { get; set; }
            public string adresse2 { get; set; }
            public string idville { get; set; }
            public string npa { get; set; }
            public string ville { get; set; }
            public string tel1 { get; set; }
            public string tel2 { get; set; }
            public string mobile { get; set; }
            public string fax { get; set; }
            public string email1 { get; set; }
            public string email2 { get; set; }
            public string siteweb { get; set; }
            public string refnom { get; set; }
            public string reftel { get; set; }
            public string reffax { get; set; }
            public string refmail { get; set; }

        }
        public void chargerInstitutions()
        {
            string sql = "SELECT paie_institutions.*, city.zip, city.cityname FROM " + dbase + ".paie_institutions " +
                "LEFT JOIN " + dbaseInit + ".city " +
                "ON city.idcity = paie_institutions.idville left join " + dbaseInit + ".language ON language.idlanguage = paie_institutions.idlanguage ORDER BY societe";
            MySqlDataReader mread;
            mscom.Connection = mscon;

            mscom.CommandType = System.Data.CommandType.Text;

            mscom.CommandText = sql;
            mread = mscom.ExecuteReader();
            while (mread.Read())
            {
                institutions.Add(new Institution()
                {
                    norefpolice = GetValue(mread, "norefpolice"),
                    adresse1 = GetValue(mread, "adresse1"),
                    adresse2 = GetValue(mread, "adresse2"),
                    co = GetValue(mread, "co"),
                    email1 = GetValue(mread, "email1"),
                    email2 = GetValue(mread, "email2"),
                    fax = GetValue(mread, "fax"),
                    mobile = GetValue(mread, "mobile"),
                    siteweb = GetValue(mread, "siteweb"),
                    idlanguage = GetValue(mread, "idlanguage"),
                    idinstitution = GetValue(mread, "idinstitution"),
                    idville = GetValue(mread, "idville"),
                    societe = GetValue(mread, "societe"),
                    npa = GetValue(mread, "zip"),
                    ville = GetValue(mread, "cityname"),
                    tel1 = GetValue(mread, "tel1"),
                    tel2 = GetValue(mread, "tel2"),
                    refnom = GetValue(mread, "refnom"),
                    reftel = GetValue(mread, "reftel"),
                    reffax = GetValue(mread, "reffax"),
                    refmail = GetValue(mread, "refmail")
                });

            }
            mread.Close();
        }
        public bool enregistrerInstitution(Institution sal, int typeMaj)
        {

            string sql = "";
            if (typeMaj == 1)
                sql = "INSERT INTO " + dbase + ".paie_institutions SET idinstitution = 0, ";
            else if (typeMaj == 2)
                sql = "UPDATE " + dbase + ".paie_institutions SET ";
            else if (typeMaj == 3)
                sql = "DELETE FROM " + dbase + ".paie_institutions ";

            if (typeMaj <= 2)
            {
                sql += " " +
                    "norefpolice ='" + sal.norefpolice + "', " +
                    "adresse1 ='" + sal.adresse1 + "', " +
                    "adresse2 ='" + sal.adresse2 + "', " +
                    "co ='" + sal.co + "', " +
                    "email1 ='" + sal.email1 + "', " +
                    "email2 ='" + sal.email2 + "', " +
                    "tel1 ='" + sal.tel1 + "', " +
                    "tel2 ='" + sal.tel2 + "', " +
                    "fax ='" + sal.fax + "', " +
                    "mobile ='" + sal.mobile + "', " +
                    "siteweb ='" + sal.siteweb + "', " +
                    "idlanguage ='" + sal.idlanguage + "', " +
                    "idville ='" + sal.idville + "', " +
                    "societe ='" + sal.societe + "', " +
                    "refnom ='" + sal.refnom + "', " +
                    "reftel ='" + sal.reftel + "', " +
                    "reffax ='" + sal.reffax + "', " +
                    "refmail ='" + sal.refmail + "' ";
            }
            sql = sql.Replace("'null'", "NULL").Replace("''", "NULL");
            if (typeMaj >= 2)
                sql += " WHERE idinstitution =" + sal.idinstitution;
            mscom.CommandText = sql;

            int nn = mscom.ExecuteNonQuery();
            if (nn > 0)
                return true;
            else
                return false;
        }

        public class Entreprise
        {
            public string identreprise { get; set; }
            public string pardefaut { get; set; }
            public string idlanguage { get; set; }
            public string societe { get; set; }
            public string co { get; set; }
            public string adresse1 { get; set; }
            public string adresse2 { get; set; }
            public string idville { get; set; }
            public string npa { get; set; }
            public string ville { get; set; }
            public string tel1 { get; set; }
            public string tel2 { get; set; }
            public string mobile { get; set; }
            public string fax { get; set; }
            public string email1 { get; set; }
            public string email2 { get; set; }
            public string siteweb { get; set; }
            public string iban { get; set; }
            public string idbanque { get; set; }
            public string comptecredit { get; set; }
            public string comptedebit { get; set; }
            public string comptecharge { get; set; }
            public string basecompta { get; set; }

        }

        public void chargerEntreprises()
        {
            string sql = "SELECT paie_entreprise.*, city.zip, city.cityname FROM " + dbase + ".paie_entreprise " +
                "LEFT JOIN " + dbaseInit + ".city " +
                "ON city.idcity = paie_entreprise.idville left join " + dbaseInit + ".language ON language.idlanguage = paie_entreprise.idlanguage ORDER BY societe";
            MySqlDataReader mread;
            mscom.Connection = mscon;

            mscom.CommandType = System.Data.CommandType.Text;

            mscom.CommandText = sql;
            mread = mscom.ExecuteReader();
            while (mread.Read())
            {
                entreprises.Add(new Entreprise()
                {
                    adresse1 = GetValue(mread, "adresse1"),
                    adresse2 = GetValue(mread, "adresse2"),
                    co = GetValue(mread, "co"),
                    iban = GetValue(mread, "iban"),
                    idbanque = GetValue(mread, "idbanque"),
                    basecompta = GetValue(mread, "basecompta"),
                    comptedebit = GetValue(mread, "comptedebit"),
                    comptecredit = GetValue(mread, "comptecredit"),
                    comptecharge = GetValue(mread, "comptecharge"),
                    email1 = GetValue(mread, "email1"),
                    email2 = GetValue(mread, "email2"),
                    fax = GetValue(mread, "fax"),
                    mobile = GetValue(mread, "mobile"),
                    siteweb = GetValue(mread, "siteweb"),
                    idlanguage = GetValue(mread, "idlanguage"),
                    identreprise = GetValue(mread, "identreprise"),
                    pardefaut = GetValue(mread, "pardefaut"),
                    idville = GetValue(mread, "idville"),
                    societe = GetValue(mread, "societe"),
                    npa = GetValue(mread, "zip"),
                    ville = GetValue(mread, "cityname"),
                    tel1 = GetValue(mread, "tel1"),
                    tel2 = GetValue(mread, "tel2")
                });

            }
            mread.Close();
        }
        public bool enregistrerEntreprise(Entreprise sal, int typeMaj)
        {

            string sql = "";
            if (typeMaj == 1)
                sql = "INSERT INTO " + dbase + ".paie_entreprise SET identreprise = 0, ";
            else if (typeMaj == 2)
                sql = "UPDATE " + dbase + ".paie_entreprise SET ";
            else if (typeMaj == 3)
                sql = "dELETE FROM " + dbase + ".paie_entreprise ";
            else if (typeMaj == 2)
                sql = "UPDATE " + dbase + ".paie_entreprise SET ";
            if (typeMaj < 3)
            {
                sql += " " +
                    "adresse1 ='" + sal.adresse1 + "', " +
                    "adresse2 ='" + sal.adresse2 + "', " +
                    "co ='" + sal.co + "', " +
                    "idbanque ='" + sal.idbanque + "', " +
                    "iban ='" + sal.iban + "', " +
                    "basecompta ='" + sal.basecompta + "', " +
                    "comptecredit ='" + sal.comptecredit + "', " +
                    "comptedebit ='" + sal.comptedebit + "', " +
                    "comptecharge ='" + sal.comptecharge + "', " +
                    "email1 ='" + sal.email1 + "', " +
                    "email2 ='" + sal.email2 + "', " +
                    "tel1 ='" + sal.tel1 + "', " +
                    "tel2 ='" + sal.tel2 + "', " +
                    "fax ='" + sal.fax + "', " +
                    "mobile ='" + sal.mobile + "', " +
                    "siteweb ='" + sal.siteweb + "', " +
                    "idlanguage ='" + sal.idlanguage + "', " +
                    "idville ='" + sal.idville + "', " +
                    "societe ='" + sal.societe + "' ";
                sql = sql.Replace("'null'", "NULL").Replace("''", "NULL");
            }
            if (typeMaj >= 2)
                sql += " WHERE identreprise =" + sal.identreprise;
            mscom.CommandText = sql;

            int nn = mscom.ExecuteNonQuery();
            if (nn > 0)
                return true;
            else
                return false;
        }

        public class Salarie
        {
            public string idsalaries { get; set; }
            public string identreprise { get; set; }
            public string idlanguage { get; set; }
            public string idpolitesse { get; set; }
            public string nom { get; set; }
            public string prenom { get; set; }
            public string co { get; set; }
            public string adresse1 { get; set; }
            public string adresse2 { get; set; }
            public string idville { get; set; }
            public string npa { get; set; }
            public string idnationalite { get; set; }
            public string tel1 { get; set; }
            public string tel2 { get; set; }
            public string mobile { get; set; }
            public string fax { get; set; }
            public string email1 { get; set; }
            public string email2 { get; set; }
            public string sexe { get; set; }
            public string etatcivil { get; set; }
            public string datenaissance { get; set; }
            public string dateentree { get; set; }
            public string datesortie { get; set; }
            public string motifsortie { get; set; }
            public string numavs { get; set; }
            public string genreavs { get; set; }
            public string genreac { get; set; }
            public string genrelaa { get; set; }
            public string genrelpp { get; set; }
            public string caissemaladie { get; set; }
            public string photo { get; set; }
            public string departementnom { get; set; }
            public string fonctionnom { get; set; }
            public string idbanque { get; set; }
            public string iban { get; set; }

        }

        public IEnumerable<Salarie> Salarie_sel(string selid = "", string selnom = "", string selprenom = "", string selidfonction = "", string seliddepartement = "")
        {
            IEnumerable<Salarie> Salarie_sel;
            Salarie_sel = salaries.Where(w => w.idsalaries == selid);

            return Salarie_sel;
        }

        public IEnumerable<Salarie> Curr_Salarie;

        public void selsalarie(string ind)
        {
            salaries_sel.Clear();
            IEnumerable<Salarie> dd = salaries.Where(w => w.idsalaries == ind.ToString());
            salaries_sel.Add(dd.First<Salarie>());
            if (salaries_sel.Count() > 0)
                salaries_sel.First<Salarie>();

        }


        public bool enregistrerSalarie(Salarie sal, int typeMaj)
        {
            string daty = "";
            try
            {
                daty = "'" + string.Format("{0:yyyy-MM-dd}", DateTime.Parse(sal.datenaissance)) + "'";
            }
            catch
            {
                daty = "NULL";
            }
            string datyentree = "";
            try
            {
                datyentree = "'" + string.Format("{0:yyyy-MM-dd}", DateTime.Parse(sal.dateentree)) + "'";
            }
            catch
            {
                datyentree = "NULL";
            }
            string datysortie = "";
            try
            {
                datysortie = "'" + string.Format("{0:yyyy-MM-dd}", DateTime.Parse(sal.datesortie)) + "'";
            }
            catch
            {
                datysortie = "NULL";
            }
            string sql = "";
            string ident = "";

            if (typeMaj == 1)
                sql = "INSERT INTO " + dbase + ".paie_salaries SET idsalaries = 0,";
            else if (typeMaj == 2)
                sql = "UPDATE " + dbase + ".paie_salaries SET ";
            else if (typeMaj == 3)
                sql = "DELETE FROM " + dbase + ".paie_salaries ";
            if (typeMaj < 3)
            {
                sql += " " +
                    "identreprise ='" + sal.identreprise + "', " +
                    "adresse1 ='" + sal.adresse1 + "', " +
                    "adresse2 ='" + sal.adresse2 + "', " +
                    "co ='" + sal.co + "', " +
                    "datenaissance =" + daty + ", " +
                    "email1 ='" + sal.email1 + "', " +
                    "email2 ='" + sal.email2 + "', " +
                    "tel1 ='" + sal.tel1 + "', " +
                    "tel2 ='" + sal.tel2 + "', " +
                    "fax ='" + sal.fax + "', " +
                    "mobile ='" + sal.mobile + "', " +
                    "idlanguage ='" + sal.idlanguage + "', " +
                    "idpolitesse ='" + sal.idpolitesse + "', " +
                    "idville ='" + sal.idville + "', " +
                    "idnationalite ='" + sal.idnationalite + "', " +
                    "nom ='" + sal.nom + "', " +
                    "prenom ='" + sal.prenom + "', " +
                    "etatcivil ='" + sal.etatcivil + "', " +
                    "dateentree =" + datyentree + ", " +
                    "datesortie =" + datysortie + ", " +
                    "motifsortie ='" + sal.motifsortie + "', " +
                    "numavs ='" + sal.numavs + "', " +
                    "genreavs ='" + sal.genreavs + "', " +
                    "genreac ='" + sal.genreac + "', " +
                    "genrelaa ='" + sal.genrelaa + "', " +
                    "genrelpp ='" + sal.genrelpp + "', " +
                    "caissemaladie ='" + sal.caissemaladie + "', " +
                    "idbanque ='" + sal.idbanque + "', " +
                    "iban ='" + sal.iban + "', " +
                    "photo ='" + @sal.photo + "', " +
                    "sexe ='" + sal.sexe + "'";
                sql = sql.Replace("'null'", "NULL").Replace("''", "NULL");
            }
            if (typeMaj >= 2)
                sql += " WHERE idsalaries =" + sal.idsalaries;
            mscom.CommandText = sql;

            int nn = mscom.ExecuteNonQuery();
            if (nn > 0)
                return true;
            else
                return false;
        }
        public void chargerSalaries(string ident)
        {
            string sql = "SELECT  date_format(datenaissance, '%d.%m.%Y') as datenaissance, depart.departementnom, fonct.fonctionnom, paie_salaries.*, city.zip, country.country as nationalitenom, country.abreviation as nationaliteabr FROM " + dbase + ".paie_salaries " +
                "LEFT JOIN " + dbase + ".paie_contratsalarie contr ON contr.idsalarie = paie_salaries.idsalaries AND contr.encours = 'O' LEFT JOIN " + dbase + ".paie_Departement depart ON depart.iddepartement = contr.iddepartement LEFT JOIN " + dbase + ".paie_fonction fonct ON fonct.idfonction = contr.idfonction " +
                "LEFT JOIN " + dbaseInit + ".city " +
                "ON city.idcity = paie_salaries.idville left join " + dbaseInit + ".country ON country.idcountry = paie_salaries.idnationalite WHERE paie_salaries.identreprise = " + ident + " ORDER BY nom asc, prenom asc";
            MySqlDataReader mread;
            mscom.Connection = mscon;

            mscom.CommandType = System.Data.CommandType.Text;

            mscom.CommandText = sql;
            mread = mscom.ExecuteReader();
            salaries.Clear();
            while (mread.Read())
            {
                salaries.Add(new Salarie() {
                    adresse1 = GetValue(mread, "adresse1"),
                    adresse2 = GetValue(mread, "adresse2"),
                    co = GetValue(mread, "co"),
                    datenaissance = GetValue(mread, "datenaissance"),
                    email1 = GetValue(mread, "email1"),
                    email2 = GetValue(mread, "email2"),
                    fax = GetValue(mread, "fax"),
                    mobile = GetValue(mread, "mobile"),
                    idlanguage = GetValue(mread, "idlanguage"),
                    idpolitesse = GetValue(mread, "idpolitesse"),
                    idsalaries = GetValue(mread, "idsalaries"),
                    identreprise = GetValue(mread, "identreprise"),
                    idville = GetValue(mread, "idville"),
                    idnationalite = GetValue(mread, "idnationalite"),
                    nom = GetValue(mread, "nom"),
                    npa = GetValue(mread, "zip"),
                    prenom = GetValue(mread, "prenom"),
                    sexe = GetValue(mread, "sexe"),
                    etatcivil = GetValue(mread, "etatcivil"),
                    tel1 = GetValue(mread, "tel1"),
                    tel2 = GetValue(mread, "tel2"),
                    caissemaladie = GetValue(mread, "caissemaladie"),
                    dateentree = GetValue(mread, "dateentree"),
                    datesortie = GetValue(mread, "datesortie"),
                    genreac = GetValue(mread, "genreac"),
                    genreavs = GetValue(mread, "genreavs"),
                    genrelaa = GetValue(mread, "genrelaa"),
                    genrelpp = GetValue(mread, "genrelpp"),
                    motifsortie = GetValue(mread, "motifsortie"),
                    numavs = GetValue(mread, "numavs"),
                    departementnom = GetValue(mread, "departementnom"),
                    fonctionnom = GetValue(mread, "fonctionnom"),
                    idbanque = GetValue(mread, "idbanque"),
                    iban = GetValue(mread, "iban"),
                    photo = @GetValue(mread, "photo")
                });

            }
            mread.Close();
        }

        
        public class Contrat
        {
            string _val;
            public string idcontrat { get; set; }
            public string idsalarie { get; set; }
            public string refcontrat { get; set; }
            public string encours { get; set; }
            public string datecontrat {
                get
                {
                    return _val;
                }
                set
                {
                    value = string.Format("{0:dd.MM.yyyy}", DateTime.Parse(value.ToString()));
                    _val = value;
                }
            }
            public string iddepartement { get; set; }
            public string departementnom { get; set; }
            public string idfonction { get; set; }
            public string fonctionnom { get; set; }
            public string horairetravail { get; set; }
            public string nbrheuretravail { get; set; }
            public string occupation { get; set; }

        }

        public bool enregistrerContrat(Contrat sal, int typeMaj, bool enc = false)
        {
            string daty = "";
            try
            {
                daty = "'" + string.Format("{0:yyyy-MM-dd}", DateTime.Parse(sal.datecontrat)) + "'";
            }
            catch
            {
                daty = "NULL";
            }
            
            string sql = "";
            string ident = "";

            if (typeMaj == 1)
            {
                sql = "INSERT INTO " + dbase + ".paie_contratsalarie SET idcontrat = 0, ";
                
            }
            else if (typeMaj == 2)
            {
                sql = "UPDATE " + dbase + ".paie_contratsalarie SET ";
                
            }
            else if (typeMaj == 3)
                sql = "DELETE FROM " + dbase + ".paie_contratsalarie ";
            if (typeMaj < 3)
            {
                sql += " " +
                    "idsalarie ='" + sal.idsalarie + "', " +
                    "refcontrat ='" + sal.refcontrat + "', " +
                    "datecontrat =" + daty + "," +
                    "horairetravail ='" + sal.horairetravail + "', " +
                    "iddepartement ='" + sal.iddepartement + "', " +
                    "idfonction ='" + sal.idfonction + "', " +
                    "nbrheuretravail ='" + sal.nbrheuretravail + "', " +
                    "occupation ='" + sal.occupation + "'";
                sql = sql.Replace("'null'", "NULL").Replace("''", "NULL");
            }
            if (typeMaj >= 2)
                sql += " WHERE idcontrat =" + sal.idcontrat;
            mscom.CommandText = sql;
            

            int nn = mscom.ExecuteNonQuery();
            if (nn > 0)
                return true;
            else
                return false;

        }

        public bool enregistrerContratEncours(Contrat sal)
        {
            
            string sql = "";
            string sql1 = "";
            string ident = "";

            
                sql = "UPDATE " + dbase + ".paie_contratsalarie SET encours = '' WHERE idsalarie =" + sal.idsalarie + ";"; 
                sql1 = "UPDATE " + dbase + ".paie_contratsalarie SET encours = 'O' WHERE idcontrat =" + sal.idcontrat;

            mscom.CommandText = sql + sql1;


            int nn = mscom.ExecuteNonQuery();
            if (nn > 0)
                return true;
            else
                return false;

        }

        public void chargerContrats(string ident)
        {
            contrats.Clear();
            if (ident == "")
                return;
            string sql = "SELECT date_format(datecontrat, '%d.%m.%Y') as datecontrat, paie_contratsalarie.*, depart.departementnom, fonct.fonctionnom FROM " + dbase + ".paie_contratsalarie " +
                "LEFT JOIN " + dbase + ".paie_Departement depart ON depart.iddepartement = paie_contratsalarie.iddepartement LEFT JOIN " + dbase + ".paie_fonction fonct ON fonct.idfonction = paie_contratsalarie.idfonction " +
                "WHERE paie_contratsalarie.idsalarie = " + ident + " ORDER BY encours DESC, datecontrat asc";
            MySqlDataReader mread;
            mscom.Connection = mscon;

            mscom.CommandType = System.Data.CommandType.Text;

            mscom.CommandText = sql;
            mread = mscom.ExecuteReader();
            while (mread.Read())
            {
                contrats.Add(new Contrat()
                {
                    idsalarie = GetValue(mread, "idsalarie"),
                    refcontrat = GetValue(mread, "refcontrat"),
                    encours = GetValue(mread, "encours"),
                    horairetravail = GetValue(mread, "horairetravail"),
                    iddepartement = GetValue(mread, "iddepartement"),
                    departementnom = GetValue(mread, "departementnom"),
                    idfonction = GetValue(mread, "idfonction"),
                    fonctionnom = GetValue(mread, "fonctionnom"),
                    nbrheuretravail = GetValue(mread, "nbrheuretravail"),
                    occupation = GetValue(mread, "occupation"),
                    idcontrat = @GetValue(mread, "idcontrat"),
                    datecontrat = GetValue(mread, "datecontrat")
                });

            }
            mread.Close();
        }

        public class PaiementSalaireEnvoie
        {
            string _val;
            public string idpaiement { get; set; }
            public string identreprise { get; set; }
            public string idsalarie { get; set; }
            public string iban { get; set; }
            public string swift { get; set; }
            public string beneficiaire { get; set; }
            public string adresse { get; set; }
            public string npaville { get; set; }
            public string annee { get; set; }
            public string mois { get; set; }
            public string envoye { get; set; }
            public string charge { get; set; }
            public decimal valeur { get; set; }
            public string datepaiement { get; set; }
            public string moistexte { get; set; }
            public string col { get; set; } 

        }
        public void chargerPaiementEnvoie(string ident, int typeaffiche = 0)
        {
            PaiementSalaireEnvoies.Clear();

            string sql = "SELECT power(mois, 6)*4500 as col, if(envoye = 0, '', lpad(envoye, 6, '0')) as envoye, if(charge = 0, '', lpad(charge, 6, '0')) as charge, LPAD(mois, 2, '0') as mois, paiement.*, " +
                "case when mois = 1 then 'Janvier' when mois = 2 then 'Février' when mois = 3 then 'Mars' when mois = 4 then 'Avril' when mois = 5 then 'Mai' when mois = 6 then 'Juin' when mois = 7 then 'Juillet' when mois = 8 then 'Août' when mois = 9 then 'Septembre' when mois = 10 then 'Octobre' when mois = 11 then 'Novembre' when mois = 12 then 'Décembre' when mois = 13 then 'Treizième' END as moistexte " +
                "from " + dbase + ".paie_paiementsalaire paiement ";
            sql += " WHERE identreprise = " + ident;
            if (typeaffiche == 0)
                sql += " AND envoye = 0 ORDER BY annee desc, mois desc, idsalarie";
            else
                sql += " AND envoye > 0 ORDER BY annee desc, mois desc, envoye desc, idsalarie";
            MySqlDataReader mread;
            mscom_sel.Connection = mscon_sel;

            mscom_sel.CommandType = System.Data.CommandType.Text;

            mscom_sel.CommandText = sql;
            mread = mscom_sel.ExecuteReader();
            while (mread.Read())
            {
                Salarie sal = salaries.Where(w => w.idsalaries == GetValue(mread, "idsalarie")).First();
                string adr = sal.adresse1;
                if (sal.adresse2 != "" && sal.adresse2 != null)
                    adr += ", " + sal.adresse2;
                City cit = cities.Where(w => w.IdVille == sal.idville).First();
                string npv = cit.Zip + " " + cit.CityName;
                chargerBanque(cond: "", idb: sal.idbanque);
                PaiementSalaireEnvoies.Add(new PaiementSalaireEnvoie()
                {
                    idpaiement = GetValue(mread, "idpaiement"),
                    idsalarie = GetValue(mread, "idsalarie"),
                    identreprise = GetValue(mread, "identreprise"),
                    annee = GetValue(mread, "annee"),
                    envoye = GetValue(mread, "envoye"),
                    charge = GetValue(mread, "charge"),
                    mois = GetValue(mread, "mois"),
                    col = GetValue(mread, "col"),
                    moistexte = GetValue(mread, "moistexte"),
                    datepaiement = GetValue(mread, "datepaiement"),
                    valeur = ToDecimal(GetValue(mread, "valeur")),
                    beneficiaire = sal.nom + " " + sal.prenom,
                    adresse = adr,
                    npaville = npv,
                    iban = sal.iban,
                    swift = banques.First().swift
                });
            }
            mread.Close();
        }

        public class PaiementSalaire
        {
            string _val;
            public string idpaiement { get; set; }
            public string identreprise { get; set; }
            public string idsalarie { get; set; }
            public string annee { get; set; }
            public string mois { get; set; }
            public decimal valeur { get; set; }
            public string datepaiement { get; set; }
            
        }



        public void chargerPaiement(string ident, string idsal = "", string ann = "", string mois = "")
        {
            PaiementSalaires.Clear();

            string sql = "SELECT * from " + dbase + ".paie_paiementsalaire paiement ";
            if (ident != "")
                sql += " WHERE identreprise = " + ident;
            else if (idsal != "")
                sql += " WHERE idsalarie = " + idsal;
            if (ann != "")
                sql += " AND annee = " + ann;
            if (mois != "")
                sql += " AND mois = " + mois;

            sql += " ORDER BY identreprise, idsalarie, annee asc, mois ASC";

            MySqlDataReader mread;
            mscom.Connection = mscon;

            mscom.CommandType = System.Data.CommandType.Text;

            mscom.CommandText = sql;
            mread = mscom.ExecuteReader();
            while (mread.Read())
            {
                PaiementSalaires.Add(new PaiementSalaire()
                {
                    idpaiement = GetValue(mread, "idpaiement"),
                    idsalarie = GetValue(mread, "idsalarie"),
                    identreprise  = GetValue(mread, "identreprise"),
                    annee = GetValue(mread, "annee"),
                    mois = GetValue(mread, "mois"),
                    datepaiement = GetValue(mread, "datepaiement"),
                    valeur = ToDecimal(GetValue(mread, "valeur"))
                });
            }
            mread.Close();
        }

        public bool enregistrerPaiement(PaiementSalaire sal, int typeMaj, bool bsalarie = false, string idsal = "")
        {

            string sql = "";
            string ident = "";
            string daty = "";
            try
            {
                daty = "'" + string.Format("{0:yyyy-MM-dd}", DateTime.Parse(sal.datepaiement)) + "'";
            }
            catch
            {
                daty = "NULL";
            }
            if (typeMaj == 1)
            {
                sql = "INSERT INTO " + dbase + ".paie_paiementsalaire SET idpaiement = 0, ";

            }
            else if (typeMaj == 2)
            {
                sql = "UPDATE " + dbase + ".paie_paiementsalaire SET ";

            }
            else if (typeMaj == 3)
                sql = "DELETE FROM " + dbase + ".paie_paiementsalaire ";
            if (typeMaj < 3)
            {
                sql += " " +
                    "idsalarie ='" + sal.idsalarie + "', " +
                    "annee ='" + sal.annee + "', " +
                    "identreprise ='" + sal.identreprise + "'," +
                    "mois ='" + sal.mois + "', " +
                    "valeur =" + GetDecimalSql(sal.valeur.ToString()) + "," +
                    "datepaiement =" +  daty ;
                sql = sql.Replace("'null'", "NULL").Replace("''", "NULL");
            }
            if (typeMaj >= 2)
            {
                if (bsalarie)
                {
                    if (idsal != "")
                        sql += " WHERE idsalarie = " + idsal;
                    else
                        sql += " WHERE idsalarie = " + sal.idsalarie;
                }
                else
                    sql += " WHERE idligne =" + sal.idpaiement;
            }
            mscom.CommandText = sql;


            int nn = mscom.ExecuteNonQuery();
            if (nn > 0)
                return true;
            else
                return false;

        }
        public class Grillesalarie
        {
            string _val;
            public string idligne { get; set; }
            public string idsalarie { get; set; }
            public string annee { get; set; }
            public string rang { get; set; }
            public string code { get; set; }
            //public string typeligne { get; set; }
            public string txtligne { get; set; }
            public string unite { get; set; }
            public string basemasse { get; set; }
            public string baseinst { get; set; }
            public string taux { get; set; }
            public string tauxemployeur { get; set; }
            public string salannu { get; set; }
            public string valeur { get; set; }
            public string repartition { get; set; }
            public string totannee { get; set; }
            public string treisieme { get; set; }
            public string totalgeneral { get; set; }
            public string idbasecalcul { get; set; }
            public string valeur1 { get; set; }
            public string valeur2 { get; set; }
            public string valeur3 { get; set; }
            public string valeur4 { get; set; }
            public string valeur5 { get; set; }
            public string valeur6 { get; set; }
            public string valeur7 { get; set; }
            public string valeur8 { get; set; }
            public string valeur9 { get; set; }
            public string valeur10 { get; set; }
            public string valeur11 { get; set; }
            public string valeur12 { get; set; }
            public string valeur13 { get; set; }
            public string deduction { get; set; }
        }

        public class GrillesalarieTmp
        {
            string _val;
            public string idligne { get; set; }
            public string idsalarie { get; set; }
            public string annee { get; set; }
            public string rang { get; set; }
            public string code { get; set; }
            //public string typeligne { get; set; }
            public string txtligne { get; set; }
            public string unite { get; set; }
            public string basemasse { get; set; }
            public string baseinst { get; set; }
            public string taux { get; set; }
            public string tauxemployeur { get; set; }
            public string salannu { get; set; }
            public string valeur { get; set; }
            public string repartition { get; set; }
            public string totannee { get; set; }
            public string treisieme { get; set; }
            public string totalgeneral { get; set; }
            public string idbasecalcul { get; set; }
            public string deduction { get; set; }
        }
        public void chargerGrillesalarie(string ident, string ann, int typec, int nmois = 0)
        {
            //typec 1 calcul, 2 donnéesgrille
            Grillesalaries.Clear();
            if (typec ==1)
            {
                generergrille(ident, ann);
                return;
            }
            if (ident == "")
                return;
            string sql = "";

            if (typec == 1)
            {
                sql = "SELECT 0 as idligne, basesal.rang, basesal.deduction, basesal.idbasecalcul, sal.idsalaries as idsalarie, " + ann + " as annee, " +
                    "concat_ws(' - '," +
                    "if (basecalc.idbasecalcul is null, if (code = 'SALMENS', 'Salaire Brut Mensuel', code) , basecalc.typeparam),  " +
                    "if (code = 'LPP', calclpp.typeplan, basecalc.txtparam)) as txtligne, " +

                    "if (basesal.partemploye = 0 AND basesal.basecalc is not null, 'Chf', '%') as unite,  " +
                    "if (basesal.basecalc is not null OR basesal.basecalc > 0, basesal.basecalc, 0) / basesal.repartition as basemasse,    " +
                    "basesal.partemploye as taux, " +
                    "if (basecalc.idbasecalcul is null, code , basecalc.typeparam) as code,  " +
                    "baseannu.salannu, " +
                    "if (basesal.partemploye = 0, basesal.basecalc / basesal.repartition, if (basesal.basecalc is not null OR basesal.basecalc > 0, basesal.basecalc, baseannu.salannu) / basesal.repartition * basesal.partemploye / 100) as valeur, " +

                    "basesal.repartition, " +

                    "if (basesal.partemploye = 0, basesal.basecalc / basesal.repartition, if (basesal.code is not null, basesal.basecalc, baseannu.salannu) / basesal.repartition * basesal.partemploye / 100) *12 as totannee, " +
                    "if (basesal.partemploye = 0, basesal.basecalc / basesal.repartition, if (basesal.code is not null, basesal.basecalc, baseannu.salannu) / basesal.repartition * basesal.partemploye / 100) *(repartition - 12) as treisieme, " +

                    "FROM " + dbase + ".paie_basesalarie " +
                    " basesal LEFT JOIn " + dbase + ".paie_salaries sal ON sal.idsalaries = basesal.idsalarie AND(basesal.code <> 'SALANNU' OR basesal.code is null)   " +
                "LEFT JOIN " + dbase + ".paie_basecalcul basecalc ON basecalc.idbasecalcul = basesal.idbasecalcul AND basesal.code = basecalc.typeparam AND basecalc.annee = " + ann + " " +
                "left join " + dbase + ".paie_calcullpp calclpp ON calclpp.idcalcullpp = basesal.idbasecalcul and basesal.code = 'LPP' " +

                "left join (select basecalc as salannu, idsalarie FROM " + dbase + ".paie_basesalarie where code = 'SALANNU') baseannu ON baseannu.idsalarie = basesal.idsalarie ";
                sql += " WHERE  sal.idsalaries = " + ident + " ORDER BY basesal.deduction, basesal.rang ASC";
            }
            else if (typec == 3)
            {
                //sql = "SELECT idligne, basesal.deduction, basesal.rang, basesal.idbasecalcul, sal.idsalaries as idsalarie, " + ann + " as annee, concat_ws(' - ',if (basecalc.idbasecalcul is null, if (code = 'SALMENS', 'Salaire Brut Mensuel', code) , basecalc.typeparam),  if (code <> 'LPP', basecalc.txtparam, calclpp.typeplan)) as txtligne, " +
                sql = "SELECT idligne, basesal.deduction, basesal.rang, basesal.idbasecalcul, sal.idsalaries as idsalarie, " + ann + " as annee, basesal.txtligne, " +

                "basesal.unite as unite, basesal.basemasse,  basesal.taux, basesal.tauxemployeur, basesal.code, basesal.salannu, valeur1 as valeur, valeur1, valeur2, valeur3, valeur4, valeur5, valeur6, valeur7, valeur8, valeur9, valeur10, valeur11, valeur12, valeur13, " +
                "basesal.repartition, (valeur1 + valeur2 + valeur3 + valeur4 + valeur5 + valeur6 + valeur7 + valeur8 + valeur9 + valeur10 + valeur11 + valeur12) as totannee, basesal.valeur13 as treisieme " +

                "FROM paie.paie_grillesalaire" +
                " basesal LEFT JOIn " + dbase + ".paie_salaries sal ON sal.idsalaries = basesal.idsalarie AND(basesal.code <> 'SALANNU' OR basesal.code is null)   " +
                "LEFT JOIN " + dbase + ".paie_basecalcul basecalc ON basecalc.idbasecalcul = basesal.idbasecalcul AND basesal.code <> 'LPP' " +
                "left join " + dbase + ".paie_calcullpp calclpp ON calclpp.idcalcullpp = basesal.idbasecalcul and basesal.code = 'LPP'  " +

                "left join (select basecalc as salannu, idsalarie FROM " + dbase + ".paie_basesalarie where code = 'SALANNU') baseannu ON baseannu.idsalarie = basesal.idsalarie ";
                sql += " WHERE basesal.annee = " + ann + " AND sal.identreprise = " + ident + " ORDER BY basesal.deduction, basesal.rang ASC";
            }
            else
            {
                sql = "SELECT idligne, basesal.deduction, basesal.rang, basesal.idbasecalcul, sal.idsalaries as idsalarie, " + ann + " as annee, basesal.txtligne, " +

                "basesal.unite as unite, basesal.basemasse,  basesal.taux, basesal.tauxemployeur, basesal.code, basesal.salannu, valeur1 as valeur, valeur1, valeur2, valeur3, valeur4, valeur5, valeur6, valeur7, valeur8, valeur9, valeur10, valeur11, valeur12, valeur13, " +
                "basesal.repartition, (valeur1 + valeur2 + valeur3 + valeur4 + valeur5 + valeur6 + valeur7 + valeur8 + valeur9 + valeur10 + valeur11 + valeur12) as totannee, basesal.valeur13 as treisieme " +

                "FROM paie.paie_grillesalaire" +
                " basesal LEFT JOIn " + dbase + ".paie_salaries sal ON sal.idsalaries = basesal.idsalarie AND(basesal.code <> 'SALANNU' OR basesal.code is null)   " +
                "LEFT JOIN " + dbase + ".paie_basecalcul basecalc ON basecalc.idbasecalcul = basesal.idbasecalcul AND basesal.code <> 'LPP' " +
                "left join " + dbase + ".paie_calcullpp calclpp ON calclpp.idcalcullpp = basesal.idbasecalcul and basesal.code = 'LPP'  " +

                "left join (select basecalc as salannu, idsalarie FROM " + dbase + ".paie_basesalarie where code = 'SALANNU') baseannu ON baseannu.idsalarie = basesal.idsalarie ";
                sql += " WHERE basesal.annee = " + ann + " AND sal.idsalaries = " + ident + " ORDER BY basesal.deduction, basesal.rang ASC";
            }

            MySqlDataReader mread;
            mscom.Connection = mscon;

            mscom.CommandType = System.Data.CommandType.Text;

            mscom.CommandText = sql;
            mread = mscom.ExecuteReader();
            
            while (mread.Read())
            {
                if (typec == 1)
                {
                    Grillesalaries.Add(new Grillesalarie()
                    {
                        idligne = GetValue(mread, "idligne"),
                        idsalarie = GetValue(mread, "idsalarie"),
                        idbasecalcul = GetValue(mread, "idbasecalcul"),
                        annee = GetValue(mread, "annee"),
                        code = GetValue(mread, "code"),
                        rang = GetValue(mread, "rang"),
                        //typeligne = GetValue(mread, "typeligne"),
                        txtligne = GetValue(mread, "txtligne"),
                        unite = GetValue(mread, "unite"),
                        basemasse = GetValue(mread, "basemasse"),
                        taux = GetValue3Virg(mread, "taux"),
                        tauxemployeur = GetValue3Virg(mread, "tauxemployeur"),
                        salannu = GetValue(mread, "salannu"),
                        valeur = GetValue3Virg(mread, "valeur"),
                        repartition = GetValue(mread, "repartition"),
                        deduction = GetValue(mread, "deduction"),
                        totannee = GetValue(mread, "totannee"),
                        treisieme = GetValue(mread, "treisieme"),
                        totalgeneral = (ToDecimal(GetValue(mread, "totannee")) + ToDecimal(GetValue(mread, "treisieme"))).ToString()
                    });
                }
                else if (typec == 3)
                {
                    string sm = "";
                    if (nmois != 0)
                        sm = nmois.ToString();
                    GrillesalarieTmps.Add(new Grillesalarie()
                    {
                        idligne = GetValue(mread, "idligne"),
                        idsalarie = GetValue(mread, "idsalarie"),
                        idbasecalcul = GetValue(mread, "idbasecalcul"),
                        annee = GetValue(mread, "annee"),
                        code = GetValue(mread, "code"),
                        rang = GetValue(mread, "rang"),
                        //typeligne = GetValue(mread, "typeligne"),
                        txtligne = GetValue(mread, "txtligne"),
                        unite = GetValue(mread, "unite"),
                        deduction = GetValue(mread, "deduction"),
                        basemasse = GetValue(mread, "basemasse"),
                        taux = GetValue3Virg(mread, "taux"),
                        tauxemployeur = GetValue3Virg(mread, "tauxemployeur"),
                        salannu = GetValue(mread, "salannu"),
                        valeur = GetValue(mread, "valeur" + sm),
                        valeur1 = GetValue(mread, "valeur1"),
                        valeur2 = GetValue(mread, "valeur2"),
                        valeur3 = GetValue(mread, "valeur3"),
                        valeur4 = GetValue(mread, "valeur4"),
                        valeur5 = GetValue(mread, "valeur5"),
                        valeur6 = GetValue(mread, "valeur6"),
                        valeur7 = GetValue(mread, "valeur7"),
                        valeur8 = GetValue(mread, "valeur8"),
                        valeur9 = GetValue(mread, "valeur9"),
                        valeur10 = GetValue(mread, "valeur10"),
                        valeur11 = GetValue(mread, "valeur11"),
                        valeur12 = GetValue(mread, "valeur12"),
                        valeur13 = GetValue(mread, "valeur13"),
                        repartition = GetValue(mread, "repartition"),
                        totannee = GetValue(mread, "totannee"),
                        treisieme = GetValue(mread, "treisieme"),
                        totalgeneral = (ToDecimal(GetValue(mread, "totannee")) + ToDecimal(GetValue(mread, "treisieme"))).ToString()
                    });
                }
                else
                {
                    Grillesalaries.Add(new Grillesalarie()
                    {
                        idligne = GetValue(mread, "idligne"),
                        idsalarie = GetValue(mread, "idsalarie"),
                        idbasecalcul = GetValue(mread, "idbasecalcul"),
                        annee = GetValue(mread, "annee"),
                        code = GetValue(mread, "code"),
                        rang = GetValue(mread, "rang"),
                        //typeligne = GetValue(mread, "typeligne"),
                        txtligne = GetValue(mread, "txtligne"),
                        unite = GetValue(mread, "unite"),
                        deduction = GetValue(mread, "deduction"),
                        basemasse = GetValue(mread, "basemasse"),
                        taux = GetValue3Virg(mread, "taux"),
                        tauxemployeur = GetValue3Virg(mread, "tauxemployeur"),
                        salannu = GetValue(mread, "salannu"),
                        valeur = GetValue(mread, "valeur"),
                        valeur1 = GetValue(mread, "valeur1"),
                        valeur2 = GetValue(mread, "valeur2"),
                        valeur3 = GetValue(mread, "valeur3"),
                        valeur4 = GetValue(mread, "valeur4"),
                        valeur5 = GetValue(mread, "valeur5"),
                        valeur6 = GetValue(mread, "valeur6"),
                        valeur7 = GetValue(mread, "valeur7"),
                        valeur8 = GetValue(mread, "valeur8"),
                        valeur9 = GetValue(mread, "valeur9"),
                        valeur10 = GetValue(mread, "valeur10"),
                        valeur11 = GetValue(mread, "valeur11"),
                        valeur12 = GetValue(mread, "valeur12"),
                        valeur13 = GetValue(mread, "valeur13"),
                        repartition = GetValue(mread, "repartition"),
                        totannee = GetValue(mread, "totannee"),
                        treisieme = GetValue(mread, "treisieme"),
                        totalgeneral = (ToDecimal(GetValue(mread, "totannee")) + ToDecimal(GetValue(mread, "treisieme"))).ToString()
                    });
                }
            }
            mread.Close();
            
        }

        public string GetValeurSalaire(string idsal, string annee, string mois, string code)
        {
            string val = "";
            Grillesalarie gs = Grillesalaries.Where(w => w.idsalarie == idsal && w.annee == annee && w.code == code).First();
            if (mois == "1")
                return gs.valeur1;
            else if (mois == "2")
                return gs.valeur2;
            else if (mois == "3")
                return gs.valeur3;
            else if (mois == "4")
                return gs.valeur4;
            else if (mois == "5")
                return gs.valeur5;
            else if (mois == "6")
                return gs.valeur6;
            else if (mois == "7")
                return gs.valeur7;
            else if (mois == "8")
                return gs.valeur8;
            else if (mois == "9")
                return gs.valeur9;
            else if (mois == "10")
                return gs.valeur10;
            else if (mois == "11")
                return gs.valeur11;
            else if (mois == "12")
                return gs.valeur12;
            else if (mois == "13")
                return gs.valeur13;
            return val;
        } 
        
        public decimal ToDecimal(string s)
        {
            decimal d = 0;
            if (s.Trim() == "")
                s = "0";
            try
            {
                d = decimal.Parse(s);
            }
            catch
            {
                d = decimal.Parse(s.Replace(".", ","));
            }
            return d;
        }

        public bool enregistrerGrillesalarie(Grillesalarie sal, int typeMaj, bool bsalarie = false, string idsal = "")
        {
            
            string sql = "";
            string ident = "";

            if (typeMaj == 1)
            {
                sql = "INSERT INTO " + dbase + ".paie_grillesalaire SET idligne = 0, ";

            }
            else if (typeMaj == 2)
            {
                sql = "UPDATE " + dbase + ".paie_grillesalaire SET ";

            }
            else if (typeMaj == 3)
                sql = "DELETE FROM " + dbase + ".paie_grillesalaire ";
            
            if (typeMaj < 3)
            {
                if (sal.deduction == "+")
                    sal.deduction = "0";
                else if (sal.deduction == "-")
                    sal.deduction = "1";
                sql += " " +
                    "idsalarie ='" + sal.idsalarie + "', " +
                    "annee ='" + sal.annee + "', " +
                    "code ='" + sal.code + "'," +
                    "rang ='" + sal.rang + "', " +
                    "txtligne ='" + sal.txtligne + "', " +
                    "unite ='" + sal.unite + "', " +
                    "basemasse ='" + sal.basemasse + "', " +
                    "baseinst ='" + sal.baseinst + "', " +
                    "taux =" + GetDecimal3VirglSql(sal.taux) + ", " +
                    "tauxemployeur =" + GetDecimal3VirglSql(sal.tauxemployeur) + ", " +
                    "salannu =" + GetDecimalSql(sal.salannu) + ", " +
                    "repartition ='" + sal.repartition + "', " +
                    "deduction =" + GetDecimalSql(sal.deduction) + "," +
                    //"treisieme =" + GetDecimalSql(sal.treisieme) + "," +
                    //"totalgeneral =" + GetDecimalSql(sal.totalgeneral) + "," +
                    "idbasecalcul ='" + sal.idbasecalcul + "', " +
                    "valeur1 =" + GetDecimalSql(sal.valeur1) + "," +
                    "valeur2 =" + GetDecimalSql(sal.valeur2) + "," +
                    "valeur3 =" + GetDecimalSql(sal.valeur3) + "," +
                    "valeur4 =" + GetDecimalSql(sal.valeur4) + "," +
                    "valeur5 =" + GetDecimalSql(sal.valeur5) + "," +
                    "valeur6 =" + GetDecimalSql(sal.valeur6) + "," +
                    "valeur7 =" + GetDecimalSql(sal.valeur7) + "," +
                    "valeur8 =" + GetDecimalSql(sal.valeur8) + "," +
                    "valeur9 =" + GetDecimalSql(sal.valeur9) + "," +
                    "valeur10 =" + GetDecimalSql(sal.valeur10) + "," +
                    "valeur11 =" + GetDecimalSql(sal.valeur11) + "," +
                    "valeur12 =" + GetDecimalSql(sal.valeur12) + "," +
                    "valeur13 =" + GetDecimalSql(sal.valeur13) + "";
                sql = sql.Replace("'null'", "NULL").Replace("''", "NULL");
            }
            if (typeMaj >= 2)
            {
                if (bsalarie)
                {
                    if (idsal != "")
                        sql += " WHERE idsalarie = " + idsal;
                    else
                        sql += " WHERE idsalarie = " + sal.idsalarie;
                }
                else
                    sql += " WHERE idligne =" + sal.idligne;
            }
            mscom.CommandText = sql;


            int nn = mscom.ExecuteNonQuery();
            if (nn > 0)
                return true;
            else
                return false;

        }

        public class FichedePaie
        {
            string _val;
            public string idsalarie { get; set; }
            public string annee { get; set; }
            public string mois { get; set; }
            public string rang { get; set; }
            public string code { get; set; }
            //public string typeligne { get; set; }
            public string txtligne { get; set; }
            public string unite { get; set; }
            public decimal basemasse { get; set; }
            public string taux { get; set; }
            public decimal valeur { get; set; }
            public string deduction { get; set; }
            public string salarieadr { get; set; }
            public string numavs { get; set; }
            public string datenaissance { get; set; }
            public string fonctionnom { get; set; }
            public string departementnom { get; set; }
            public string datepaiement { get; set; }
            public string nummois { get; set; }

        }

        public string GetForma(object ss)
        {
            if (ss == "")
                ss = 0;
            decimal d = ToDecimal(ss.ToString());
            return string.Format("{0:0.00;-0.00;0.00}", d).Replace(",", ".");
        }
        public string[] listemois = new string[] { "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre", "Treizième" };
        
        public void chargerFichedePaie(string idsal,string annee, string mois)
        {
            string[] h = new string[] { "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre", "Treizième" };
            string smois = h[int.Parse(mois.Replace("valeur", "")) - 1];
            string sql = "SELECT " + mois.Replace("valeur", "") + " as nummois, paie_salaries.idsalaries as idsalarie, concat_ws('\n', politesseadr, concat(nom, ' ', prenom), co, adresse1, adresse2, concat(zip, ' ', city.cityname)) as salarieadr, '" + smois + "' as mois, gril.txtligne, if(gril.basemasse>0, gril.basemasse, grilSalmens." + mois + " )  as basemasse, gril.*, gril.code, gril.annee, gril.rang, gril." + mois + " as valeur, date_format(datenaissance, '%d.%m.%Y') as datenaissance, depart.departementnom, pol.politesseadr, fonct.fonctionnom, paie_salaries.*, city.zip, country.country as nationalitenom, country.abreviation as nationaliteabr FROM " + dbase + ".paie_salaries " +
                "LEFT JOIN " + dbase + ".paie_grillesalaire gril ON gril.idsalarie = paie_salaries.idsalaries AND gril.annee = '"+ annee + "' LEFT JOIN " + dbase + ".paie_contratsalarie contr ON contr.idsalarie = paie_salaries.idsalaries AND contr.encours = 'O' " +
                "LEFT JOIN " + dbase + ".paie_grillesalaire grilSalmens ON grilSalmens.idsalarie = paie_salaries.idsalaries AND grilSalmens.annee = '" + annee + "' AND grilSalmens.code = 'SALMENS' " + 
                "LEFT JOIN " + dbase + ".paie_Departement depart ON depart.iddepartement = contr.iddepartement LEFT JOIN " + dbase + ".paie_fonction fonct ON fonct.idfonction = contr.idfonction " +
                "LEFT JOIN " + dbaseInit + ".city " +
                "ON city.idcity = paie_salaries.idville left join " + dbaseInit + ".country ON country.idcountry = paie_salaries.idnationalite LEFT JOIN " + dbaseInit + ".typepolitesse pol ON pol.idpolitesse = paie_salaries.idpolitesse WHERE paie_salaries.idsalaries = " + idsal + " ORDER BY mois, deduction,rang";
            MySqlDataReader mread;
            mscom.Connection = mscon;

            mscom.CommandType = System.Data.CommandType.Text;

            mscom.CommandText = sql;
            mread = mscom.ExecuteReader();
            //FichedePaies.Clear();
            while (mread.Read())
            {
                FichedePaies.Add(new FichedePaie()
                {
                    salarieadr = GetValue(mread, "salarieadr"),
                    idsalarie = GetValue(mread, "idsalarie"),
                    datenaissance = GetValue(mread, "datenaissance"),
                    annee = GetValue(mread, "annee"),
                    mois = GetValue(mread, "mois"),
                    code = GetValue(mread, "code"),
                    rang = GetValue(mread, "rang"),
                    basemasse = ToDecimal(GetValue(mread, "basemasse")),
                    taux = GetValue3Virg(mread, "taux"),//typeligne = GetValue(mread, "typeligne"),
                    txtligne = GetValue(mread, "txtligne"),
                    deduction = GetValue(mread, "deduction"),
                    unite = GetValue(mread, "unite"),
                    valeur = (ToDecimal(GetValue(mread, "valeur"))),
                    numavs = GetValue(mread, "numavs"),
                    departementnom = GetValue(mread, "departementnom"),
                    nummois = GetValue(mread, "nummois"),
                    fonctionnom = GetValue(mread, "fonctionnom")
                    
                });

            }
            mread.Close();

        }

        public class DonneeSalarieResume
        {
            string _val;
            public string idsalarie { get; set; }
            public string txtresume { get; set; }
            public string txtparam { get; set; }
            public string unite { get; set; }
            public string idbasecalcul { get; set; }
            public string code { get; set; }
            public string basemasse { get; set; }
            public string taux { get; set; }
            public string tauxemployeur { get; set; }
            public string deduction { get; set; }
            public string rang { get; set; }
            public string repartition { get; set; }


        }

        public void chargerDonneeSalarieResume(string ident, bool toGrille = false) 
        {
            if (ident.Trim() == "")
                return;
            string sql = "SELECT sal.idsalaries as idsalarie, basesal.rang, basesal.idbasecalcul, basesal.code, basesal.repartition, if (basesal.deduction = '1', '-', '+') as deduction,  if(basecalc.idbasecalcul is null, if (code = 'SALANNU', 'Salaire Brut Annuel', if (code = 'SALMENS', 'Salaire Brut Mensuel', code)) , basecalc.typeparam) as txtresume, " +
                "if(code = 'LPP', calclpp.typeplan, basecalc.txtparam) as txtparam, if (basesal.basecalc * 1 >0, 'Chf', '%') as unite, " +
                //"if (basesal.code is not null, if(basesal.code = 'SALMENS', basesal.basecalc/basesal.repartition, basesal.basecalc), 0) as basemasse, " +
                "if (basesal.basecalc > 0, if(basesal.code = 'SALMENS', basesal.basecalc/basesal.repartition,basesal.basecalc), basesal.basecalc) as basemasse, " +
                "basesal.partemploye as taux, basesal.partemployeur as tauxemployeur FROM " + dbase + ".paie_basesalarie basesal LEFT JOIn " + dbase + ".paie_salaries sal ON sal.idsalaries = basesal.idsalarie " +
                "LEFT JOIN " + dbase + ".paie_basecalcul basecalc ON basecalc.idbasecalcul = basesal.idbasecalcul AND basesal.code = basecalc.typeparam left join " + dbase + ".paie_calcullpp calclpp ON calclpp.idcalcullpp = basesal.idbasecalcul and basesal.code is not null ";
            if (toGrille)
                sql += "WHERE sal.idsalaries = " + ident + "  ORDER BY basesal.deduction, basesal.rang ASC";
            else
                sql += "WHERE sal.idsalaries = " + ident + " and (basesal.partemploye> 0 OR basesal.code = 'SALMENS' or basesal.code = 'SALANNU') ORDER BY basesal.deduction, basesal.rang ASC";
            MySqlDataReader mread;
            mscom.Connection = mscon;

            mscom.CommandType = System.Data.CommandType.Text;

            mscom.CommandText = sql;
            mread = mscom.ExecuteReader();
            ObservableCollection<DonneeSalarieResume> res;
            if (toGrille)
            {
                res = DonneeSalarieResumesTmp;
                DonneeSalarieResumesTmp.Clear();
            }
            else
            {
                res = DonneeSalarieResumes;
                DonneeSalarieResumes.Clear();
            }
            while (mread.Read())
            {
                /*decimal salannu = 0;
                string basem = "";
                if (toGrille)
                {
                    if (GetValue(mread, "txtparam") == "SALANNU")
                    {
                        salannu = ToDecimal(GetValue(mread, "basemasse"));
                    }
                    basem = GetValue(mread, "basemasse");
                    decimal d = 0;
                    decimal.TryParse(GetValue(mread, "basemasse"), out d);
                    if (d == 0)
                    {

                    }
                }*/
                
                res.Add(new DonneeSalarieResume()
                {

                    idsalarie = GetValue(mread, "idsalarie"),
                    txtresume = GetValue(mread, "txtresume"),
                    txtparam = GetValue(mread, "txtparam"),
                    unite = GetValue(mread, "unite"),
                    idbasecalcul = GetValue(mread, "idbasecalcul"),
                    taux = GetValue3Virg(mread, "taux"),
                    tauxemployeur = GetValue3Virg(mread, "tauxemployeur"),
                    deduction = GetValue(mread, "deduction"),
                    rang = GetValue(mread, "rang"),
                    code = GetValue(mread, "code"),
                    repartition = GetValue(mread, "repartition"),
                    basemasse = GetValue(mread, "basemasse")
                });

            }
            mread.Close();
        }

        public void generergrille(string sidsalarie, string ann)
        {
            chargerDonneeSalarieResume(sidsalarie, true);
            foreach (DonneeSalarieResume rec in DonneeSalarieResumesTmp)
            {
                if (rec.code == "SALANNU")
                    continue;
                decimal dbasem = 0;
                decimal dbaseminst = 0;
                decimal.TryParse(rec.basemasse, out dbasem);
                decimal.TryParse(rec.basemasse, out dbaseminst);
                string sunite = "%";

                if (dbasem == 0) //base calcul à calculer
                {
                    /*if (rec.basemasse == "LPP")
                    {
                        GenreLPPs.Where(d => d.)
                    }
                    else*/
                        dbasem = ToDecimal(calculbasem(rec.basemasse));
                }
                else
                {
                    if (ToDecimal(rec.taux) == 0)
                        sunite = "Chf";
                }
                if (dbaseminst == 0) //base calcul à calculer
                {
                    /*if (rec.basemasse == "LPP")
                    {
                        GenreLPPs.Where(d => d.)
                    }
                    else*/
                    dbaseminst = ToDecimal(calculbasem(rec.basemasse, true));
                }
                decimal dvaleur = 0;
                
                if (ToDecimal(rec.taux) > 0)
                {
                    dvaleur = (dbasem / ToDecimal(rec.repartition)) * ToDecimal(rec.taux) / 100;
                }
                string sligne = "";
                if (rec.code.Contains("SAL") == false)
                    sligne = rec.txtresume + " - " + rec.txtparam;
                else
                {
                    sligne = rec.txtresume;
                    dvaleur = dbasem;
                }
                Grillesalaries.Add(new Grillesalarie()
                {
                    idligne = "0",
                    idsalarie = sidsalarie,
                    idbasecalcul = rec.idbasecalcul,
                    annee = ann,
                    code = rec.code,
                    rang = rec.rang,
                    
                    //typeligne = GetValue(mread, "typeligne"),
                    txtligne = sligne,
                    unite = sunite,
                    basemasse = dbasem.ToString(),
                    baseinst = dbaseminst.ToString(),
                    taux = rec.taux,
                    tauxemployeur = rec.tauxemployeur,
                    salannu = "0",
                    valeur = string.Format("{0:#0.0000;-#.0000;'0.0000'}", dvaleur),
                    repartition = rec.repartition,
                    deduction = rec.deduction,
                    totannee = string.Format("{0:#0.0000;-#.0000;'0.0000'}", dvaleur * 12),
                    treisieme = string.Format("{0:#0.0000;-#.0000;'0.0000'}", dvaleur * (ToDecimal(rec.repartition) - 12)),
                    totalgeneral = string.Format("{0:#0.0000;-#.0000;'0.0000'}", (dvaleur * 12) + (dvaleur * (ToDecimal(rec.repartition) - 12)))
                });
            }
        }

        private string calculbasem(string scode, bool binst = false)
        {
            string sbasem = "";
            decimal dbasem = 0;
            IEnumerable<DonneeSalarieResume> rec = DonneeSalarieResumesTmp.Where(d => d.code == scode);
            decimal.TryParse(rec.First().basemasse, out dbasem);
            if (dbasem == 0) //base calcul à calculer
            {
                dbasem = ToDecimal(calculbasem(rec.First().basemasse));
            }
            if (ToDecimal(rec.First().taux) > 0)
            {
                if (binst)
                    dbasem = (dbasem * ToDecimal(rec.First().taux) / 100) + (dbasem * ToDecimal(rec.First().tauxemployeur) / 100);
                else
                   dbasem = (dbasem * ToDecimal(rec.First().taux) / 100);
            }

            return dbasem.ToString();
        }

        public class DonneeBaseSalarie
        {
            string _val;
            public string idbasesalarie { get; set; }
            public string idsalarie { get; set; }
            public string rang { get; set; }
            public string idbasecalcul { get; set; }
            public string code { get; set; }
            public string basecalc { get; set; }
            public string partemployeur { get; set; }
            public string partemploye { get; set; }
            public string repartition { get; set; }
            public string deduction { get; set; }

        }

        public void chargerDonneeBaseSalaries(string idsal, string ident = "")
        {
            if (idsal.Trim() == "" && ident == "")
                return;
            if (ident != "")
                DonneeBaseSalarieTmps.Clear();
            else
                DonneeBaseSalaries.Clear();
            string sql = "SELECT if (paie_basesalarie.deduction = '1', '-', '+') as deduction, paie_basesalarie.idbasesalarie, paie_basesalarie.idsalarie, paie_basesalarie.rang, paie_basesalarie.idbasecalcul, paie_basesalarie.repartition, paie_basesalarie.code, paie_basesalarie.basecalc, paie_basesalarie.partemploye, paie_basesalarie.partemployeur, paie_basesalarie.deduction  FROM " + dbase + ".paie_basesalarie LEFT JOIn " + dbase + ".paie_salaries sal ON sal.idsalaries = paie_basesalarie.idsalarie " +
                "LEFT JOIN " + dbase + ".paie_basecalcul basecalc ON basecalc.idbasecalcul = paie_basesalarie.idbasecalcul ";
            if (ident != "")
                sql += "INNER JOIN " + dbase + ".paie_entreprise entrep ON entrep.identreprise = sal.identreprise AND entrep.identreprise = " + ident + " ORDER BY sal.idsalaries, paie_basesalarie.deduction, paie_basesalarie.rang ASC";
            else
                sql += "WHERE sal.idsalaries = " + idsal + " ORDER BY sal.idsalaries, paie_basesalarie.deduction, paie_basesalarie.rang ASC";
            MySqlDataReader mread;
            mscom.Connection = mscon;

            mscom.CommandType = System.Data.CommandType.Text;

            mscom.CommandText = sql;
            mread = mscom.ExecuteReader();
            while (mread.Read())
            {
                if (ident != "")
                {
                    DonneeBaseSalarieTmps.Add(new DonneeBaseSalarie()
                    {

                        idbasesalarie = GetValue(mread, "idbasesalarie"),
                        idsalarie = GetValue(mread, "idsalarie"),
                        rang = GetValue(mread, "rang"),
                        idbasecalcul = GetValue(mread, "idbasecalcul"),
                        code = GetValue(mread, "code"),
                        basecalc = GetValue(mread, "basecalc"),
                        repartition = GetValue(mread, "repartition"),
                        deduction = GetValue(mread, "deduction"),
                        partemployeur = GetValue3Virg(mread, "partemployeur"),
                        partemploye = GetValue3Virg(mread, "partemploye")
                    });
                }
                else
                {
                    DonneeBaseSalaries.Add(new DonneeBaseSalarie()
                    {

                        idbasesalarie = GetValue(mread, "idbasesalarie"),
                        idsalarie = GetValue(mread, "idsalarie"),
                        rang = GetValue(mread, "rang"),
                        idbasecalcul = GetValue(mread, "idbasecalcul"),
                        code = GetValue(mread, "code"),
                        basecalc = GetValue(mread, "basecalc"),
                        repartition = GetValue(mread, "repartition"),
                        deduction = GetValue(mread, "deduction"),
                        partemployeur = GetValue3Virg(mread, "partemployeur"),
                        partemploye = GetValue3Virg(mread, "partemploye")
                    });
                }
            }
            mread.Close();
        }
        public bool enregistrerDonneeBaseSalarie(DonneeBaseSalarie sal, int typeMaj)
        {

            string sql = "";
            string ident = "";
            string ded = "0";
            if (sal.deduction == "-")
                ded = "1";
            if (typeMaj == 1)
            {
                sql = "INSERT INTO " + dbase + ".paie_basesalarie SET idbasesalarie = 0, ";

            }
            else if (typeMaj == 2)
            {
                sql = "UPDATE " + dbase + ".paie_basesalarie SET ";

            }
            else if (typeMaj == 3)
                sql = "DELETE FROM " + dbase + ".paie_basesalarie ";
            if (typeMaj < 3)
            {
                sql += " " +
                    "idsalarie ='" + sal.idsalarie + "', " +
                    "rang ='" + sal.rang + "', " +
                    "code ='" + sal.code + "', " +
                    "basecalc ='" + sal.basecalc + "', " +
                    "repartition ='" + sal.repartition + "', " +
                    "deduction ='" + ded + "', " +
                    "partemployeur ='" + GetDecimal3VirglSql(sal.partemployeur) + "', " +
                    "partemploye ='" + GetDecimal3VirglSql(sal.partemploye) + "', " +
                    "idbasecalcul ='" + sal.idbasecalcul + "'";
                    
                sql = sql.Replace("'null'", "NULL").Replace("''", "NULL");
            }
            if (typeMaj >= 2)
                sql += " WHERE idbasesalarie =" + sal.idbasesalarie;
            mscom.CommandText = sql;


            int nn = mscom.ExecuteNonQuery();
            if (nn > 0)
                return true;
            else
                return false;

        }

        public bool supprimerDonneeBaseSalarie(string idsal)
        {

            string sql = "";
            string ident = "";

            
            sql = "DELETE FROM " + dbase + ".paie_basesalarie WHERE idsalarie =" + idsal;
            mscom.CommandText = sql;


            int nn = mscom.ExecuteNonQuery();
            if (nn > 0)
                return true;
            else
                return false;

        }
        public class TauxEntrepriseCopy
        {
            string _val;
            public string idbasecalcul { get; set; }
            public bool selectionne { get; set; }
            public string identreprise { get; set; }
            public string rang { get; set; }
            public string idinstitution { get; set; }
            public string instnom { get; set; }
            public string annee { get; set; }
            public string typeparam { get; set; }
            public string txtparam { get; set; }
            public string tauxemployeur { get; set; }
            public string tauxemploye { get; set; }
            public string conditioncalc { get; set; }
            public string comptecompta { get; set; }
            public string formulebase { get; set; }
            public string deduction { get; set; }

        }
        public class TauxEntreprise
        {
            string _val;
            public string idbasecalcul { get; set; }
            public bool selectionne { get; set; }
            public string identreprise { get; set; }
            public string rang { get; set; }
            public string idinstitution { get; set; }
            public string instnom { get; set; }
            public string annee { get; set; }
            public string typeparam { get; set; }
            public string txtparam { get; set; }
            public string tauxemployeur { get; set; }
            public string tauxemploye { get; set; }
            public string conditioncalc { get; set; }
            public string comptecompta { get; set; }
            public string formulebase { get; set; }
            public string deduction { get; set; }

        }

        public void chargerTauxEntreprises(string ident, string annee)
        {
            TauxEntreprises.Clear();
            if (ident.Trim() == "")
                return;
            string sql = "SELECT inst.societe as instnom, if(deduction ='1', '-', '+') as deduction, inst.idinstitution, paie_basecalcul.* FROM " + dbase + ".paie_basecalcul LEFT JOIn " + dbase + ".paie_institutions inst ON inst.idinstitution = paie_basecalcul.idinstitution " +
                "WHERE paie_basecalcul.identreprise = " + ident + " ORDER BY rang ASC, typeparam, txtparam";
            MySqlDataReader mread;
            mscom.Connection = mscon;

            mscom.CommandType = System.Data.CommandType.Text;

            mscom.CommandText = sql;
            mread = mscom.ExecuteReader();
            while (mread.Read())
            {
                TauxEntreprises.Add(new TauxEntreprise()
                {

                    idbasecalcul = GetValue(mread, "idbasecalcul"),
                    selectionne = false,
                    rang = GetValue(mread, "rang"),
                    identreprise = GetValue(mread, "identreprise"),
                    idinstitution = GetValue(mread, "idinstitution"),
                    instnom = GetValue(mread, "instnom"),
                    annee = GetValue(mread, "annee"),
                    typeparam = GetValue(mread, "typeparam"),
                    txtparam = GetValue(mread, "txtparam"),
                    tauxemployeur = GetValue3Virg(mread, "tauxemployeur"),
                    tauxemploye = GetValue3Virg(mread, "tauxemploye"),
                    conditioncalc = GetValue(mread, "conditioncalc"),
                    comptecompta = GetValue(mread, "comptecompta"),
                    deduction = GetValue(mread, "deduction"),
                    formulebase = GetValue(mread, "formulebase")
                });

            }
            mread.Close();
        }

        public int enregistrerTauxEntreprise(TauxEntreprise sal, int typeMaj)
        {
            
            string sql = "";
            string ident = "";
            string ded = "0";
            if (sal.deduction == "-")
                ded = "1";
            if (typeMaj == 1)
            {
                sql = "INSERT INTO " + dbase + ".paie_basecalcul SET idbasecalcul = 0, ";

            }
            else if (typeMaj == 2)
            {
                sql = "UPDATE " + dbase + ".paie_basecalcul SET ";

            }
            else if (typeMaj == 3)
                sql = "DELETE FROM " + dbase + ".paie_basecalcul ";
            if (typeMaj < 3)
            {
                sql += " " +
                    "identreprise ='" + sal.identreprise + "', " +
                    "rang ='" + sal.rang + "', " +
                    "idinstitution ='" + sal.idinstitution + "', " +
                    "annee ='" + sal.annee + "', " +
                    "typeparam ='" + sal.typeparam + "'," +
                    "txtparam ='" + sal.txtparam + "', " +
                    "deduction ='" + ded + "', " +
                    "comptecompta ='" + sal.comptecompta + "', " +
                    "formulebase ='" + sal.formulebase + "', " +
                    "tauxemployeur =" + GetDecimal3VirglSql(sal.tauxemployeur) + ", " +
                    "tauxemploye =" + GetDecimal3VirglSql(sal.tauxemploye) + ", " +
                    "conditioncalc ='" + sal.conditioncalc + "'";
                sql = sql.Replace("'null'", "NULL").Replace("''", "NULL");
            }
            if (typeMaj >= 2)
                sql += " WHERE idbasecalcul =" + sal.idbasecalcul;
            mscom.CommandText = sql;


            int nn = mscom.ExecuteNonQuery();
            if (typeMaj == 2 && nn > 0)
                nn = 1;
            else if (typeMaj == 1)
                nn = int.Parse(mscom.LastInsertedId.ToString());
            return nn;

        }

        public class Banque
        {
            string _val;
            public string idbanque { get; set; }
            public string nocb { get; set; }
            public string nosic { get; set; }
            public string swift { get; set; }
            public string nomabrege { get; set; }
            public string nombanque { get; set; }
            public string adresse { get; set; }
            public string casepostale { get; set; }
            public string npa { get; set; }
            public string ville { get; set; }

        }

        public void chargerBanque(string cond = "", string idb = "", Boolean tout = false)
        {
            banques.Clear();
            if (idb == null)
                return;
            string sql = "SELECT cpta_banque.* FROM " + dbaseInit + ".cpta_banque ";
            if (idb != "")
                sql += " WHERE idbanque = " + idb;
            else if (cond != "")
                sql += " WHERE nocb LIKE '%" + cond + "%' OR nosic LIKE '%" + cond + "%' OR swift LIKE '%" + cond + "%' OR nomabrege LIKE '%" + cond + "%' ORDER BY SWIFT ";
            else if (tout == true)
                sql += " ORDER BY SWIFT ";
            MySqlDataReader mread;
            mscom.Connection = mscon;

            mscom.CommandType = System.Data.CommandType.Text;

            mscom.CommandText = sql;
            mread = mscom.ExecuteReader();
            while (mread.Read())
            {
                banques.Add(new Banque()
                {

                    idbanque = GetValue(mread, "idbanque"),
                    adresse = GetValue(mread, "adresse"),
                    casepostale = GetValue(mread, "casepostale"),
                    nocb = GetValue(mread, "nocb"),
                    nomabrege = GetValue(mread, "nomabrege"),
                    nombanque = GetValue(mread, "nombanque"),
                    nosic = GetValue(mread, "nosic"),
                    npa = GetValue(mread, "npa"),
                    swift = GetValue3Virg(mread, "swift"),
                    ville = GetValue3Virg(mread, "ville")
                });

            }
            mread.Close();
        }

        public string GetDecimalSql(string s)
        {
            decimal d = 0;
            if (s == null || s.Trim() == "")
                s = "0";
            try
            {
                s = s.Replace(",", ".");
                d = decimal.Parse(s);
            }
            catch
            {
                s = s.Replace(".", ",");
                d = decimal.Parse(s);
            }
            return string.Format("{0:0.00;-0.00;0.00}", d).Replace(",", ".");
        }
        public string GetDecimal3VirglSql(string s)
        {
            decimal d = 0;
            if (s == null || s.Trim() == "")
                s = "0";
            try
            {
                s = s.Replace(",", ".");
                d = decimal.Parse(s);
            }
            catch
            {
                s = s.Replace(".", ",");
                d = decimal.Parse(s);
            }
            return string.Format("{0:0.0000;-0.0000;0.0000}", d).Replace(",", ".");
        }
        public class IniFile
        {
            private string fileName;

            public IniFile(string fileName)
            {
                if (!File.Exists(fileName))
                    throw new FileNotFoundException(fileName + " n'éxiste pas", fileName);
                this.fileName = fileName;
            }

            [DllImport("kernel32")]
            private static extern int GetPrivateProfileString(string section,
              string key, string def, StringBuilder retVal, int size, string filePath);

            [DllImport("kernel32")]
            private static extern int GetPrivateProfileSection(string section, IntPtr lpReturnedString,
              int nSize, string lpFileName);

            public string ReadString(string section, string key)
            {
                const int bufferSize = 255;
                StringBuilder temp = new StringBuilder(bufferSize);
                GetPrivateProfileString(section, key, "", temp, bufferSize, fileName);
                return temp.ToString();
            }

            public string[] ReadSection(string section)
            {
                const int bufferSize = 2048;

                StringBuilder returnedString = new StringBuilder();

                IntPtr pReturnedString = Marshal.AllocCoTaskMem(bufferSize);
                try
                {
                    int bytesReturned = GetPrivateProfileSection(section, pReturnedString, bufferSize, fileName);

                    for (int i = 0; i < bytesReturned - 1; i++)
                        returnedString.Append((char)Marshal.ReadByte(new IntPtr((uint)pReturnedString + (uint)i)));
                }
                finally
                {
                    Marshal.FreeCoTaskMem(pReturnedString);
                }

                string sectionData = returnedString.ToString();
                return sectionData.Split('\0');
            }
        }

        public string dbaseInit = "";
        public string dbase = "";
        public bool connecter()
        {
            IniFile Inif = new IniFile(Environment.CurrentDirectory + "\\Deltareal.ini");

            string ConStr = "";
            mscon.Close();

            dbaseInit = Inif.ReadString("BDD", "databaseName");
            dbase = Inif.ReadString("BDD", "databaseNamePaie");

            ConStr = "SERVER=" + Inif.ReadString("BDD", "hostname") + "; Allow User Variables=True; DATABASE=" + dbaseInit +
                "; UID=" + Inif.ReadString("BDD", "user") + "; PASSWORD=" + Inif.ReadString("BDD", "password") + "; PORT=" + Inif.ReadString("BDD", "port") + ";default command timeout=600;ConnectionTimeout=600";

            mscon.ConnectionString = ConStr;
            mscon_sel.ConnectionString = ConStr;

            //server=localhost;User Id=root;password=za;Persist Security Info=True;database=deltareal

            try
            {
                //Cursor.Current = Cursors.WaitCursor;

                mscon.Open();
                mscon_sel.Open();
                Properties.Settings.Default["deltarealConnectionString"] = ConStr;

                return true;
            }
            catch
            {
                //var result = MessageBox.Show(this, "Connexion échouée ! Veuillez vérifier les paramètres !", "Connexion action la Base de Données.");
                MessageBox.Show("Connexion à la base échouée ! Veuillez vérifier les paramètres !", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
                return false;

                //System.Windows.Forms.Application.Exit();
            }

        }

        public MySqlDataReader selectCom(MySqlCommand com, string sql)
        {
            
            
            if (com.Connection.State != System.Data.ConnectionState.Open)
                com.Connection.Open();
            MySqlDataReader mread;
            com.CommandType = System.Data.CommandType.Text;
            com.CommandText = sql;
            mread = com.ExecuteReader();
            return mread;
        }
        public string executeSQLTexte(string sql)
        {


            if (mscom.Connection.State != System.Data.ConnectionState.Open)
                mscom.Connection.Open();
            
            mscom.CommandType = System.Data.CommandType.Text;
            mscom.CommandText = sql;
            int d = mscom.ExecuteNonQuery();
            return mscom.LastInsertedId.ToString();
            
        }

        public string executeSQL(string table, string champ, string val, int typereq, string cond)
        {
            val = val.Replace("\"", "'");
            string sz = "";
            string[] schamp = champ.Split((char)(','));
            string[] sval = val.Split((char)('$'));
            string ret = "";
            if (typereq == 2) // ajout
            {

                sz += "INSERT INTO " + table + " (" + champ + ") VALUES (";
                val = "";

                for (int i = 0; i < schamp.Length; i++)
                {
                    if (sval[i].Trim().ToLower() != "null")
                    {
                        if (schamp[i].Contains("daty") || schamp[i].Contains("date"))
                            sval[i] = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(sval[i]));
                        if (sval[i].Contains("##"))
                            sval[i] = sval[i].Trim().Replace("##", "");
                        else
                            sval[i] = "\"" + sval[i].Trim() + "\"";
                    }
                    //sval[i] = "\"" + sval[i].Trim() + "\"";
                    if (i == 0)
                        sz += sval[i];
                    else
                        sz += ", " + sval[i];
                }
                sz += ")";

            }
            else if (typereq == 1) //modif
            {
                sz = "UPDATE " + table + " SET ";
                for (int i = 0; i < schamp.Length; i++)
                {
                    if (sval[i].Trim().ToLower() != "null")
                    {
                        if (sval[i].Contains("##"))
                            sval[i] = sval[i].Trim().Replace("##", "");
                        else
                            sval[i] = "\"" + sval[i].Trim() + "\"";
                    }
                    if (i == 0)
                        sz += schamp[i] + "=" + sval[i];
                    else
                        sz += ", " + schamp[i] + "=" + sval[i];
                }
                if (cond.Trim() != "")
                    sz += " WHERE " + cond;
            }
            mscom.CommandText = sz;
            int f = mscom.ExecuteNonQuery();
            if (f < 0)
            {
                MessageBox.Show("Erreur ! Veuillez demander l'admin !");
                return "-1";
            }
            else
            {
                if (typereq == 2)
                    return mscom.LastInsertedId.ToString();
                else
                    return "1";
            }
        }
        public string ValeurParCond(string sTable, string sChamp, string sChampResult, string sCondition, string requeteavant = "")
        {
            if (requeteavant != "")
            {
                mscom.CommandText = requeteavant;
                mscom.ExecuteNonQuery();
            }
            mscom.CommandText = "SELECT " + sChamp + " FROM " + sTable + " WHERE " + sCondition; // + " order by (" + sChampResult + " <> '' and " + sChampResult + " is not null) desc limit 1";
            //QC.FetchAll = true;
            MySqlDataReader Qr = mscom.ExecuteReader();
            string Ret = "";
            if (Qr.Read())
            {
                if (Qr.GetValue(Qr.GetOrdinal(sChampResult)).GetType().Name != typeof(DBNull).Name)
                    Ret = Qr.GetString(Qr.GetOrdinal(sChampResult));
                Qr.Close();
                return Ret;
            }
            else
            {
                Qr.Close();
                return Ret;
            }
        }
        public ObservableCollection<City> cities = new ObservableCollection<City>();

        public class City
        {
            public string CityName { get; set; }
            public string Zip { get; set; }
            public string IdVille { get; set; }
        }
        public void chargerNpaVille()
        {
            //Loaded
            MySqlDataReader mread;
            mscom.Connection = mscon;

            mscom.CommandType = System.Data.CommandType.Text;

            mscom.CommandText = "SELECT idcity, if(isnull(cityname), '', cityname) as cityname, if(isnull(zip), '', zip) as zip FROM " + dbaseInit + ".city ORDER BY zip"; // WHERE " + cond;
            mread = mscom.ExecuteReader();
            cities.Clear();
            while (mread.Read())
            {
                cities.Add(new City() { Zip = mread.GetString("zip"), CityName = mread.GetString("cityname"), IdVille = mread.GetString("idcity") });
            }

            mread.Close();
        }

        public ObservableCollection<Politesse> politesses = new ObservableCollection<Politesse>();

        public class Politesse
        {
            public string IdPolitesse { get; set; }
            public string PolitesseNom { get; set; }
            public string PolitesseLettre { get; set; }
            public string PolitesseAdr { get; set; }
            public string IdLangue { get; set; }
        }

        public void chargerPolitesse()
        {
            MySqlDataReader mread;
            mscom.Connection = mscon;

            mscom.CommandType = System.Data.CommandType.Text;

            mscom.CommandText = "SELECT idpolitesse, idlangue, politesse as politessenom, politesseadr, politesselettre FROM " + dbaseInit + ".typepolitesse ORDER BY idpolitesse"; // WHERE " + cond;
            mread = mscom.ExecuteReader();
            politesses.Clear();
            while (mread.Read())
            {
                politesses.Add(new Politesse() { IdPolitesse = mread.GetString("idpolitesse"), PolitesseAdr = GetValue(mread, "politesseadr"), PolitesseLettre = GetValue(mread, "politesselettre"), PolitesseNom = GetValue(mread, "politessenom"), IdLangue = GetValue(mread, "idlangue") });
            }

            mread.Close();
        }

        public ObservableCollection<Nationalite> nationalites = new ObservableCollection<Nationalite>();

        public class Nationalite
        {
            public string IdNationalite { get; set; }
            public string NationaliteNom { get; set; }
            public string NationaliteAbr { get; set; }
        }

        public void chargerNationalite()
        {
            MySqlDataReader mread;
            mscom.Connection = mscon;

            mscom.CommandType = System.Data.CommandType.Text;

            mscom.CommandText = "SELECT idcountry as idnationalite, country as nationalitenom, abreviation as nationaliteabr FROM " + dbaseInit + ".country ORDER BY idnationalite"; // WHERE " + cond;
            mread = mscom.ExecuteReader();
            nationalites.Clear();
            while (mread.Read())
            {
                nationalites.Add(new Nationalite() { IdNationalite = mread.GetString("idnationalite"), NationaliteAbr = GetValue(mread, "nationaliteabr"), NationaliteNom = GetValue(mread, "nationalitenom") });
            }

            mread.Close();
        }

        public ObservableCollection<Language> langues = new ObservableCollection<Language>();
        public class Language
        {
            public string LanguageNom { get; set; }
            public string Abre { get; set; }
            public string Letter { get; set; }
            public string IdLanguage { get; set; }
        }
        public void chargerLangue()
        {
            MySqlDataReader mread;
            mscom.Connection = mscon;

            mscom.CommandType = System.Data.CommandType.Text;

            mscom.CommandText = "SELECT idlanguage, language as languagenom, abre, letter FROM " + dbaseInit + ".language ORDER BY idlanguage"; // WHERE " + cond;
            mread = mscom.ExecuteReader();
            langues.Clear();
            while (mread.Read())
            {
                langues.Add(new Language() { LanguageNom = mread.GetString("languagenom"), Abre = GetValue(mread, "abre"), Letter = GetValue(mread, "letter"), IdLanguage = GetValue(mread, "idlanguage") });
            }

            mread.Close();
        }

        public ObservableCollection<Departement> departements = new ObservableCollection<Departement>();

        public class Departement
        {
            public string IdDepartement { get; set; }
            public string DepartementNom { get; set; }
            public string IdEntreprise { get; set; }
        }

        public void chargerDepartement()
        {
            MySqlDataReader mread;
            mscom.Connection = mscon;

            mscom.CommandType = System.Data.CommandType.Text;

            mscom.CommandText = "SELECT iddepartement, departementnom, identreprise FROM " + dbase + ".paie_departement ORDER BY departementnom"; // WHERE " + cond;
            mread = mscom.ExecuteReader();
            departements.Clear();
            while (mread.Read())
            {
                departements.Add(new Departement() { DepartementNom = mread.GetString("departementnom"), IdEntreprise = GetValue(mread, "identreprise"),  IdDepartement = GetValue(mread, "iddepartement") });
            }

            mread.Close();
        }

        public int enregistrerDepartement(Departement dep, int typeMaj)
        {

            string sql = "";
            if (typeMaj == 1)
                sql = "INSERT INTO " + dbase + ".paie_departement SET iddepartement = 0, ";
            else if (typeMaj == 2)
                sql = "UPDATE " + dbase + ".paie_departement SET ";
            else if (typeMaj == 3)
                sql = "DELETE FROM " + dbase + ".paie_departement WHERE iddepartement = " + dep.IdDepartement;

            if (typeMaj < 3)
                sql += " " +
                    "identreprise = " + dep.IdEntreprise + ", " +
                    "departementnom ='" + dep.DepartementNom + "' ";
            sql = sql.Replace("'null'", "NULL").Replace("''", "NULL");
            if (typeMaj == 2)
                sql += " WHERE iddepartement =" + dep.IdDepartement;
            mscom.CommandText = sql;
            int nn = 0;
            if (mscom.ExecuteNonQuery() > 0)
            {
                if (typeMaj == 1)
                    nn = int.Parse(mscom.LastInsertedId.ToString());
                else
                    nn = 1;
            }
            //if (nn > 0)
                return nn;
            //else
            //    return false;
        }

        public int enregistrerFonction(Fonction dep, int typeMaj, string iddep = "")
        {

            string sql = "";
            if (typeMaj == 1)
                sql = "INSERT INTO " + dbase + ".paie_fonction SET idfonction = 0, ";
            else if (typeMaj == 2)
                sql = "UPDATE " + dbase + ".paie_fonction SET ";
            else if (typeMaj == 3)
            {
                sql = "DELETE FROM " + dbase + ".paie_fonction ";
                if (iddep == "")
                    sql += " WHERE idfonction = " + dep.IdFonction;
                else
                    sql += " WHERE iddepartement = " + iddep;
            }
            if (typeMaj < 3)
                sql += " " +
                    "iddepartement = " + dep.IdDepartement + ", " +
                    "fonctionnom ='" + dep.FonctionNom + "' ";
            sql = sql.Replace("'null'", "NULL").Replace("''", "NULL");
            if (typeMaj == 2)
                sql += " WHERE idfonction =" + dep.IdFonction;
            mscom.CommandText = sql;
            int nn = 0;
            if (mscom.ExecuteNonQuery() > 0)
            {
                if (typeMaj == 1)
                    nn = int.Parse(mscom.LastInsertedId.ToString());
                else
                    nn = 1;
            }
            //if (nn > 0)
            return nn;
            //else
            //    return false;
        }

        public ObservableCollection<Fonction> fonctions = new ObservableCollection<Fonction>();

        public class Fonction
        {
            public string IdFonction { get; set; }
            public string IdDepartement { get; set; }
            public string FonctionNom { get; set; }
        }

        public void chargerFonction()
        {
            MySqlDataReader mread;
            mscom.Connection = mscon;

            mscom.CommandType = System.Data.CommandType.Text;

            mscom.CommandText = "SELECT idfonction, iddepartement, fonctionnom FROM " + dbase + ".paie_fonction ORDER BY iddepartement, fonctionnom"; // WHERE " + cond;
            mread = mscom.ExecuteReader();
            fonctions.Clear();
            while (mread.Read())
            {
                fonctions.Add(new Fonction() { FonctionNom = GetValue(mread,"fonctionnom"), IdFonction = mread.GetString("idfonction"), IdDepartement = GetValue(mread, "iddepartement") });
            }

            mread.Close();
        }
        public string ViderP(object S)
        {
            if (S.ToString().Trim() == "")
                S = "0";
            //return string.Format("{0:#0.00;- #.00;'0.00'}", S).Replace(",", ".");
            return string.Format("{0:0.00;-#.00;'0.00'}", S).Replace(",", ".").Replace(" ", "'");
        }

        public string GetValue(MySqlDataReader tmp, string Champ)
        {
            string ret = "";
            //if (Tab.GetFieldType(Tab.GetOrdinal(nomCol)) == typeof(string))

            if ((tmp.GetFieldType(tmp.GetOrdinal(Champ)) == typeof(DateTime)))
            {
                if ((tmp.GetValue(tmp.GetOrdinal(Champ)).ToString() == ""))
                    ret = "NULL#";
                else
                    ret = string.Format("{0:dd.MM.yyyy}", tmp.GetValue(tmp.GetOrdinal(Champ)));
            }
            else if ((tmp.GetValue(tmp.GetOrdinal(Champ)).ToString() != "") && (tmp.GetFieldType(tmp.GetOrdinal(Champ)) == typeof(double) || tmp.GetFieldType(tmp.GetOrdinal(Champ)) == typeof(float) || tmp.GetFieldType(tmp.GetOrdinal(Champ)) == typeof(decimal)))
            {
                ret = string.Format("{0:#0.00;-#.00;'0.00'}",(tmp.GetDouble(tmp.GetOrdinal(Champ))));
                ret = ViderP(ret);
            }
            else
            {
                if (tmp.GetValue(tmp.GetOrdinal(Champ)).ToString() == "")
                    ret = "";
                else
                    ret = tmp.GetValue(tmp.GetOrdinal(Champ)).ToString();
            }
            return ret;
        }
        public string GetValue3Virg(MySqlDataReader tmp, string Champ)
        {
            string ret = "";
            //if (Tab.GetFieldType(Tab.GetOrdinal(nomCol)) == typeof(string))

            if ((tmp.GetFieldType(tmp.GetOrdinal(Champ)) == typeof(DateTime)))
            {
                if ((tmp.GetValue(tmp.GetOrdinal(Champ)).ToString() == ""))
                    ret = "NULL#";
                else
                    ret = string.Format("{0:dd.MM.yyyy}", tmp.GetValue(tmp.GetOrdinal(Champ)));
            }
            else if ((tmp.GetValue(tmp.GetOrdinal(Champ)).ToString() != "") && (tmp.GetFieldType(tmp.GetOrdinal(Champ)) == typeof(double) || tmp.GetFieldType(tmp.GetOrdinal(Champ)) == typeof(float) || tmp.GetFieldType(tmp.GetOrdinal(Champ)) == typeof(decimal)))
            {
                ret = string.Format("{0:#0.0000;-#.0000;'0.0000'}", (tmp.GetDouble(tmp.GetOrdinal(Champ))));
                ret = ViderP(ret);
            }
            else
            {
                if (tmp.GetValue(tmp.GetOrdinal(Champ)).ToString() == "")
                    ret = "";
                else
                    ret = tmp.GetValue(tmp.GetOrdinal(Champ)).ToString();
            }
            return ret;
        }

        public string GetValueRound(MySqlDataReader tmp, string Champ)
        {
            string ret = "";
            //if (Tab.GetFieldType(Tab.GetOrdinal(nomCol)) == typeof(string))

            if ((tmp.GetFieldType(tmp.GetOrdinal(Champ)) == typeof(DateTime)))
            {
                if ((tmp.GetValue(tmp.GetOrdinal(Champ)).ToString() == ""))
                    ret = "NULL#";
                else
                    ret = string.Format("{0:dd.MM.yyyy}", tmp.GetValue(tmp.GetOrdinal(Champ)));
            }
            else if ((tmp.GetValue(tmp.GetOrdinal(Champ)).ToString() != "") && (tmp.GetFieldType(tmp.GetOrdinal(Champ)) == typeof(double) || tmp.GetFieldType(tmp.GetOrdinal(Champ)) == typeof(float) || tmp.GetFieldType(tmp.GetOrdinal(Champ)) == typeof(decimal)))
            {
                ret = string.Format("{0:#0.00;-#.00;'0.00'}", Math.Round(tmp.GetDouble(tmp.GetOrdinal(Champ)) / 0.05) * 0.05);
                ret = ViderP(ret);
            }
            else
            {
                if (tmp.GetValue(tmp.GetOrdinal(Champ)).ToString() == "")
                    ret = "";
                else
                    ret = tmp.GetValue(tmp.GetOrdinal(Champ)).ToString();
            }
            return ret;
        }
        public void InitConnection()
        {
            //MainWindow')
            string ConStr = "";

            ConStr = "SERVER=localhost; Allow User Variables=True; DATABASE=deltapaie; UID=root; PASSWORD=realvista; PORT=3306;default command timeout=600;ConnectionTimeout=600";

            mscon.ConnectionString = ConStr;
            mscon.Open();
        }

        public string Maxsuivant(string sTable, string sChamp, string scond)
        {
            //int iCount = GetRecordCount(QC, sTable, "");
            mscom.CommandText = "SELECT (max(" + sChamp + ") + 1) as tmp FROM " + sTable;
            if (scond != "")
                mscom.CommandText = mscom.CommandText + " WHERE " + scond;
            MySql.Data.MySqlClient.MySqlDataReader Q = mscom.ExecuteReader();
            //Q.Read();
            string Ret = "";

            //if (Q.RecordCount != 0)
            //if (iCount != 0)
            if (Q.Read() && Q.GetValue(Q.GetOrdinal("tmp")).ToString() != "")
            {
                Ret = Q.GetValue(Q.GetOrdinal("tmp")).ToString();
            }
            else
                Ret = "1";
            Q.Close();
            return Ret;
        }
        public void TestKey(KeyPressEventArgs e, bool Virgule)
        {

            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 13 || e.KeyChar == 8 || e.KeyChar.ToString() == "." || e.KeyChar.ToString() == "," || e.KeyChar.ToString() == "-")
            {
                if (e.KeyChar.ToString() == "," || e.KeyChar.ToString() == ".")
                {
                    if (Virgule == true)
                        e.KeyChar = char.Parse(Application.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    else
                    {
                        e.KeyChar = (char)0;
                        e.Handled = true;
                    }
                }
            }
            else
            {
                e.KeyChar = (char)0;
                e.Handled = true;
            }
        }

    }
}
