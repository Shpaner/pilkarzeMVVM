using PilkarzeMVVM.Model;
using PilkarzeMVVM.ViewModel.Commands;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace PilkarzeMVVM.ViewModel
{
    class MainWindowVM : BaseVM
    {
        public const ushort WIEK = 22;
        public const ushort WAGA = 75;
        private const string dane_sciezka = "data.xml";

        #region properties

        private bool pokazToolTip_imie;
        public bool PokazToolTip_imie
        {
            get { return pokazToolTip_imie; }
            set
            {
                pokazToolTip_imie = value;
                OnPropertyChanged(nameof(PokazToolTip_imie));
            }
        }

        private bool pokazToolTip_nazwisko;
        public bool PokazToolTip_nazwisko
        {
            get { return pokazToolTip_nazwisko; }
            set
            {
                pokazToolTip_nazwisko = value;
                OnPropertyChanged(nameof(PokazToolTip_nazwisko));
            }
        }

        private string imie;
        public string Imie
        {
            get { return imie; } 
            set
            {
                imie = value;
                PokazToolTip_imie = false;
                OnPropertyChanged(nameof(Imie));
            }
        }

        private string nazwisko;
        public string Nazwisko
        {
            get { return nazwisko; }
            set
            {
                nazwisko = value;
                PokazToolTip_nazwisko = false;
                OnPropertyChanged(nameof(Nazwisko));
            }
        }

        private ushort wiek;
        public ushort Wiek
        {
            get { return wiek; }
            set
            {
                wiek = value;
                OnPropertyChanged(nameof(Wiek));
            }
        }

        private ushort waga;
        public ushort Waga
        {
            get { return waga; }
            set
            {
                waga = value;
                OnPropertyChanged(nameof(Waga));
            }
        }

        private Pilkarz wybranyPilkarz;
        public Pilkarz WybranyPilkarz
        {
            get { return wybranyPilkarz; }
            set
            {
                wybranyPilkarz = value;
                OnPropertyChanged(nameof(WybranyPilkarz));
                if (value != null)
                    UstalWidok(value.Imie, value.Nazwisko, value.Waga, value.Wiek);
            }
        }

        public ObservableCollection<Pilkarz> Pilkarze { get; set; }

        public int WybranyIndex { get; set; }

        #endregion

        #region commands

        private ICommand dodaj_Command;
        public ICommand Dodaj_Command
        {
            get
            {
                return dodaj_Command ?? (dodaj_Command = new CommandHandler(() => DodajPilkarza(), () => { return true; }));
            }
        }

        private ICommand usun_Command;
        public ICommand Usun_Command
        {
            get { return usun_Command ?? (usun_Command = new CommandHandler(() => { Pilkarze.Remove(WybranyPilkarz); UstalWidok(); }, () => { return WybranyPilkarz != null; })); }
        }



        private ICommand edytuj_Command;
        public ICommand Edytuj_Command
        {
            get
            { return edytuj_Command ?? (edytuj_Command = new CommandHandler(() => EditPlayer(), () => { return WybranyPilkarz != null; }));}
        }

        #endregion

        #region methods

        public MainWindowVM()
        {
            Pilkarze = new ObservableCollection<Pilkarz>();
            OdzyskajDane();
            UstalWidok();
            PokazToolTip_imie = false;
            PokazToolTip_nazwisko = false;
        }

        ~MainWindowVM()
        {
            Zapisz();
        }

        private void DodajPilkarza()
        {
            Imie = Imie.Trim();
            Nazwisko = Nazwisko.Trim();
            PokazToolTip_nazwisko = Nazwisko.Length == 0;
            PokazToolTip_imie = Imie.Length == 0;

            if (!(PokazToolTip_imie || PokazToolTip_nazwisko))
            {
                Pilkarz nowy = new Pilkarz(Imie, Nazwisko, Wiek, Waga);
                if (!CzyZawiera(nowy))
                {
                    Pilkarze.Add(nowy);
                    UstalWidok();
                }

                else { MessageBox.Show("Taki piłkarz jest już w bazie"); }
            }
        }

        private void EditPlayer()
        {
            Pilkarz nowy = new Pilkarz(Imie, Nazwisko, Wiek, Waga);

            if (!CzyZawiera(nowy))
            {
                int index = WybranyIndex;
                if (MessageBox.Show($"Czy na pewno chcesz zmienić dane?", "Edytowanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Pilkarze.Remove(wybranyPilkarz);
                    Pilkarze.Insert(index, nowy);
                    UstalWidok();
                }
            }

            else { MessageBox.Show("Taki piłkarz jest już w bazie"); }
        }

        private bool CzyZawiera(Pilkarz p)
        {
            foreach (Pilkarz pilkarz in Pilkarze)
            {
                if (pilkarz.Equals(p))
                    return true;
            }
            return false;
        }

        private void UstalWidok(string imie = "", string nazwisko = "", ushort waga = WAGA, ushort wiek = WIEK)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Waga = waga;
            Wiek = wiek;
        }

        private void OdzyskajDane()
        {
            if (File.Exists(dane_sciezka))
            {
                StreamReader reader = new StreamReader(dane_sciezka);
                XmlSerializer xData = new XmlSerializer(typeof(ObservableCollection<Pilkarz>));
                ObservableCollection<Pilkarz> dane = xData.Deserialize(reader) as ObservableCollection<Pilkarz>;
                foreach (Pilkarz pilkarz in dane)
                    Pilkarze.Add(pilkarz);
            }
        }

        private void Zapisz()
        {
            File.Delete(dane_sciezka);
            Stream stream = File.OpenWrite(dane_sciezka);
            XmlSerializer xData = new XmlSerializer(typeof(ObservableCollection<Pilkarz>));
            xData.Serialize(stream, Pilkarze);
            stream.Close();
        }

        #endregion
    }
}
