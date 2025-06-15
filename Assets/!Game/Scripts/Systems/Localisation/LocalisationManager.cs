using R3;

public enum Localisation
{
    en,
    ru,
}


public class LocalisationManager
{
    private static LocalisationManager _instance;
    public static LocalisationManager Instance
    {
        get
        {
            _instance ??= new LocalisationManager();
            return _instance;
        }
    }

    private LocalisationManager()
    {
        CurrentLocalisation = new(SaveManager.Instance.Data.Localisation);
    }

    public ReactiveProperty<Localisation> CurrentLocalisation { get; set; }

}