namespace Cmas.DataLayers.CouchDb.Contracts.Dtos
{
    /// <summary>
    /// Стоимость договора
    /// </summary>
    public class AmountDto
    {
        // Валюта
        public string CurrencySysName;

        // значение
        public double Value = 0;
    }
}
