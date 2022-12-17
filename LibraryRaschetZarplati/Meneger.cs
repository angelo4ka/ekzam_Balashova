using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRaschetZarplati
{
    public class Meneger
    {
        private int id;
        private int juniorMinimum;
        private int middleMinimum;
        private int seniorMinimum;
        private double analysisCoefficient;
        private double installationCoefficient;
        private double supportCoefficient;
        private double timeCoefficient;
        private double difficultyCoefficient;
        private double toMoneyCoefficient;

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public int JuniorMinimum
        {
            get
            {
                return juniorMinimum;
            }
            set
            {
                juniorMinimum = value;
            }
        }
        public int MiddleMinimum
        {
            get
            {
                return middleMinimum;
            }
            set
            {
                middleMinimum = value;
            }
        }
        public int SeniorMinimum
        {
            get
            {
                return seniorMinimum;
            }
            set
            {
                seniorMinimum = value;
            }
        }
        public double AnalysisCoefficient
        {
            get
            {
                return analysisCoefficient;
            }
            set
            {
                analysisCoefficient = value;
            }
        }
        public double InstallationCoefficient
        {
            get
            {
                return installationCoefficient;
            }
            set
            {
                installationCoefficient = value;
            }
        }
        public double SupportCoefficient
        {
            get
            {
                return supportCoefficient;
            }
            set
            {
                supportCoefficient = value;
            }
        }
        public double TimeCoefficient
        {
            get
            {
                return timeCoefficient;
            }
            set
            {
                timeCoefficient = value;
            }
        }
        public double DifficultyCoefficient
        {
            get
            {
                return difficultyCoefficient;
            }
            set
            {
                difficultyCoefficient = value;
            }
        }
        public double ToMoneyCoefficient
        {
            get
            {
                return toMoneyCoefficient;
            }
            set
            {
                toMoneyCoefficient = value;
            }
        }

        public Meneger(int id, int juniorMinimum, int middleMinimum, int seniorMinimum,
            double analysisCoefficient, double installationCoefficient, double supportCoefficient,
            double timeCoefficient, double difficultyCoefficient, double toMoneyCoefficient)
        {
            this.id = id;
            this.juniorMinimum = juniorMinimum;
            this.middleMinimum = middleMinimum;
            this.seniorMinimum = seniorMinimum;
            this.analysisCoefficient = analysisCoefficient;
            this.installationCoefficient = installationCoefficient;
            this.supportCoefficient = supportCoefficient;
            this.timeCoefficient = timeCoefficient;
            this.difficultyCoefficient = difficultyCoefficient;
            this.toMoneyCoefficient = toMoneyCoefficient;
        }
    }
}
