using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace DeltaSalaire
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    /// 
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Il y a eu un erreur, veuillez informer le résponsable : " + e.Exception.Message + (char)13 + e.Exception.TargetSite.ToString(), "Erreur ", MessageBoxButton.OK, MessageBoxImage.Warning);
            e.Handled = true;
        }
        private void PART_ContentHost_GotFocus(object sender, RoutedEventArgs e)
        {
            ((sender as ScrollViewer).TemplatedParent as TextBox).SelectAll(); 
        }
        private void PART_ContentHost_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string[] ss = new string[] { "nbr", "num", "tel", "mtt", "fax", "mobile", "taux", "annee", "compta" };
            bool trouve = false;
            foreach (string f in ss)
            {
                if (((sender as ScrollViewer).TemplatedParent as TextBox).Name.Contains(f))
                {
                    trouve = true;
                    break; 
                }

            }
            if (!trouve)
                return; 
            if ((e.Key == System.Windows.Input.Key.NumPad0 || e.Key == System.Windows.Input.Key.NumPad1 || e.Key == System.Windows.Input.Key.NumPad2 || e.Key == System.Windows.Input.Key.NumPad3 ||
                e.Key == System.Windows.Input.Key.NumPad4 || e.Key == System.Windows.Input.Key.NumPad5 || e.Key == System.Windows.Input.Key.NumPad6 || e.Key == System.Windows.Input.Key.NumPad7 ||
                e.Key == System.Windows.Input.Key.NumPad8 || e.Key == System.Windows.Input.Key.NumPad9 || e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Back || 
                e.Key == System.Windows.Input.Key.OemComma || e.Key == System.Windows.Input.Key.Decimal || e.Key == System.Windows.Input.Key.Tab) )
            
            {
                /*if (e.Key == System.Windows.Input.Key.OemComma || e.Key == System.Windows.Input.Key.Decimal)
                {
                    //if (Virgule == true)
                        e.Source = char.Parse(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    //else
                    //{
                      //  e.KeyChar = (char)0;
                       // e.Handled = true;
                    //}
                }*/
            }
            else
            {
                //e.Source = (char)0;
                e.Handled = true;
            }
        

        }

        
    }
}
