using System; // Necesario para DateTime

namespace DeepCore.Client.Misc
{
    public static class OtherUtil // Mantenemos el nombre para que coincida con la llamada
    {
        /// <summary>
        /// Calcula la diferencia entre dos fechas y la devuelve como un string formateado (ej: "X Ys, YMs, ZDs").
        /// </summary>
        /// <param name="dob">La fecha de inicio (Date of Birth).</param>
        /// <returns>Un string representando la edad o duración.</returns>
        public static string ToAgeString(DateTime dob)
        {
            DateTime today = DateTime.Today; // Fecha actual (solo día, sin hora)
            int months = today.Month - dob.Month; // Diferencia inicial de meses
            int years = today.Year - dob.Year;    // Diferencia inicial de años

            // Si el día actual es anterior al día de 'dob' en el mes,
            // significa que aún no se ha completado el último mes.
            if (today.Day < dob.Day)
            {
                months--; // Restamos un mes
            }

            // Si la resta de meses dio negativo, significa que aún no se ha completado el último año.
            if (months < 0)
            {
                years--;      // Restamos un año
                months += 12; // Sumamos 12 a los meses (ej: -1 mes se convierte en 11 meses)
            }

            // Calculamos los días restantes desde la fecha 'dob' ajustada por los años y meses completos.
            // dob.AddMonths(years * 12 + months) nos da la fecha equivalente a 'dob' pero en el mes actual calculado.
            int days = (today - dob.AddMonths(years * 12 + months)).Days;

            // Formateamos el string final, añadiendo 's' para plurales.
            // Usando string interpolado para claridad:
            return $"{years} Y{(years == 1 ? "" : "s")}, {months}M{(months == 1 ? "" : "s")}, {days}D{(days == 1 ? "" : "s")}";

            // Formato original con string.Format (funciona igual):
            // return string.Format("{0} Y{1}, {2}M{3} {4}D{5}",
            //                      years, (years == 1) ? "" : "s",
            //                      months, (months == 1) ? "" : "s",
            //                      days, (days == 1) ? "" : "s");
        }

        // --- NO NECESITAS NINGÚN OTRO MÉTODO O CAMPO DE LA CLASE OtherUtil ORIGINAL ---
    }
}