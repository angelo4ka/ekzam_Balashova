using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRaschetZarplati
{
    public class RaschetZarplati
    {
        /// <summary>
        /// Определение минимальной суммы (гарантируемого минимума) зарплаты исполнителя
        /// в зависимости от его ранга в компании
        /// </summary>
        /// <param name="rangIspolnitela">Ранг исполнителя</param>
        /// <param name="meneger">Менеджер, под руководством которого находится исполнитель</param>
        /// <returns>Гарантируемый минимум зарплаты</returns>
        public static int OpredelitMinimymZarplati(string rangIspolnitela, Meneger meneger)
        {
            switch (rangIspolnitela)
            {
                case "junior":
                    return meneger.JuniorMinimum;

                case "middle":
                    return meneger.MiddleMinimum;

                case "senior":
                    return meneger.SeniorMinimum;

                default:
                    return 0;
            }
        }

        /// <summary>
        /// Определение коэффициента характера выполненных работ в зависимости от типа работы
        /// </summary>
        /// <param name="haracterRaboti">Характер выполненных работ</param>
        /// <param name="meneger">Менеджер, под руководством которого находится исполнитель</param>
        /// <returns>Коэффициент характера выполненных работ</returns>
        private static double OpredelitCoefficientTipaRaboti(string haracterRaboti, Meneger meneger)
        {
            switch (haracterRaboti)
            {
                case "анализ и проектирование":
                    return meneger.AnalysisCoefficient;

                case "установка оборудования":
                    return meneger.InstallationCoefficient;

                case "техническое обслуживание и сопровождение":
                    return meneger.SupportCoefficient;

                default:
                    return 0;
            }
        }

        /// <summary>
        /// Расчёт оплаты за выполненную залачу
        /// </summary>
        /// <param name="meneger">Менеджер, под руководством которого находится исполнитель</param>
        /// <param name="zadacha">Выполненная задача</param>
        /// <returns></returns>
        private static double OplataZadachi(Meneger meneger, Zadacha zadacha)
        {
            double stoimostVremeni = zadacha.Time * meneger.TimeCoefficient;
            double stoimostSlogjnosti = zadacha.Difficulty * meneger.DifficultyCoefficient;
            double coefficentHaracteraRaboti = OpredelitCoefficientTipaRaboti(zadacha.WorkType, meneger);
            double stoimostTipaZadachi = (stoimostVremeni + stoimostSlogjnosti) * coefficentHaracteraRaboti;
            double denegjniyVesZadachi = (meneger.TimeCoefficient + meneger.DifficultyCoefficient + coefficentHaracteraRaboti) * meneger.ToMoneyCoefficient;

            double stoimostZadachi = stoimostTipaZadachi + denegjniyVesZadachi;


            return stoimostZadachi;
        }

        /// <summary>
        /// Расчёт заработной платы исполнителя
        /// </summary>
        /// <param name="minZarplata">Гарантированный минимум заработной платы за месяц</param>
        /// <param name="meneger">Менеджер, под руководством которого находится исполнитель</param>
        /// <param name="vipolnennieZadachi">Список выполненных задач</param>
        /// <returns></returns>
        public static double RaschitatZarplaty(int minZarplata, Meneger meneger, List<Zadacha> vipolnennieZadachi)
        {
            double zarplata = minZarplata;

            foreach (Zadacha zadacha in vipolnennieZadachi)
            {
                zarplata += OplataZadachi(meneger, zadacha);
            }


            return zarplata;
        }


    }
}
