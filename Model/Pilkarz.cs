namespace PilkarzeMVVM.Model
{
#pragma warning disable CS0659
    public class Pilkarz
#pragma warning restore CS0659
    {
        #region properties
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public ushort Wiek { get; set; }
        public ushort Waga { get; set; }
        #endregion

        #region constructors
        public Pilkarz(string imie, string nazwisko, ushort wiek, ushort waga)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Wiek = wiek;
            Waga = waga;
        }
        public Pilkarz(string imie, string nazwisko) : this(imie, nazwisko, 30, 75) { }
        public Pilkarz() { }
        #endregion

        #region methods
        public override string ToString()
        {
            return $"{Imie} {Nazwisko} lat: {Wiek} waga: {Waga} kg";
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
                return false;

            else
            {
                Pilkarz pilkarz = (Pilkarz)obj;
                if (Imie.ToLower().Equals(pilkarz.Imie.ToLower()) && Wiek.Equals(pilkarz.Wiek) && Nazwisko.ToLower().Equals(pilkarz.Nazwisko.ToLower()) && Waga.Equals(pilkarz.Waga))
                    return true;

                else
                    return false;
            }
        }
        #endregion
    }
}

