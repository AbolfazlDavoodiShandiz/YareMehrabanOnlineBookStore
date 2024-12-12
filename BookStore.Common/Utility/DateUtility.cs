namespace BookStore.Common.Utility
{
    public static class DateUtility
    {
        public static string GetPersianMonthName(int month)
        {
            string name = string.Empty;

            if (month > 12 || month < 1)
            {
                throw new ArgumentException("Month");
            }

            switch (month)
            {
                case 1:
                    name = "فروردین";
                    break;
                case 2:
                    name = "اردیبهشت";
                    break;
                case 3:
                    name = "خرداد";
                    break;
                case 4:
                    name = "تیر";
                    break;
                case 5:
                    name = "مرداد";
                    break;
                case 6:
                    name = "شهریور";
                    break;
                case 7:
                    name = "مهر";
                    break;
                case 8:
                    name = "آبان";
                    break;
                case 9:
                    name = "آذر";
                    break;
                case 10:
                    name = "دی";
                    break;
                case 11:
                    name = "بهمن";
                    break;
                case 12:
                    name = "اسفند";
                    break;
            }

            return name;
        }
    }
}
