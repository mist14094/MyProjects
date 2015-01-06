using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class TbcAnalytics
    {
        private double _totalAverageNicotine;
        private double _totalAverageTSugar;
        private double _totalAverageRSugar;

        private double _latestNicotine;
        private DateTime _dateLatestNicotine;
        private double _latestTSugar;
        private DateTime _dateLatestTSugar;
        private double _latestRSugar;
        private DateTime _dateLatestRSugar;

        private double _highestNicotine;
        private DateTime _dateHighestNicotine;
        private double _highestTSugar;
        private DateTime _dateHighestTSugar;
        private double _highestRSugar;
        private DateTime _dateHighestRSugar;

        private double _lowestNicotine;
        private DateTime _dateLowestNicotine;
        private double _lowestTSugar;
        private DateTime _dateLowestTSugar;
        private double _lowestRSugar;
        private DateTime _dateLowestRSugar;

        public TbcAnalytics()
        {
        }

        public double TotalAverageNicotine
        {
            get { return _totalAverageNicotine; }
            set { _totalAverageNicotine = value; }
        }

        public double TotalAverageTSugar
        {
            get { return _totalAverageTSugar; }
            set { _totalAverageTSugar = value; }
        }

        public double TotalAverageRSugar
        {
            get { return _totalAverageRSugar; }
            set { _totalAverageRSugar = value; }
        }

        public double LatestNicotine
        {
            get { return _latestNicotine; }
            set { _latestNicotine = value; }
        }

        public double LatestTSugar
        {
            get { return _latestTSugar; }
            set { _latestTSugar = value; }
        }

        public double LatestRSugar
        {
            get { return _latestRSugar; }
            set { _latestRSugar = value; }
        }

        public double HighestNicotine
        {
            get { return _highestNicotine; }
            set { _highestNicotine = value; }
        }

        public DateTime DateHighestNicotine
        {
            get { return _dateHighestNicotine; }
            set { _dateHighestNicotine = value; }
        }

        public double HighestTSugar
        {
            get { return _highestTSugar; }
            set { _highestTSugar = value; }
        }

        public DateTime DateHighestTSugar
        {
            get { return _dateHighestTSugar; }
            set { _dateHighestTSugar = value; }
        }

        public double HighestRSugar
        {
            get { return _highestRSugar; }
            set { _highestRSugar = value; }
        }

        public DateTime DateHighestRSugar
        {
            get { return _dateHighestRSugar; }
            set { _dateHighestRSugar = value; }
        }

        public double LowestNicotine
        {
            get { return _lowestNicotine; }
            set { _lowestNicotine = value; }
        }

        public DateTime DateLowestNicotine
        {
            get { return _dateLowestNicotine; }
            set { _dateLowestNicotine = value; }
        }

        public double LowestTSugar
        {
            get { return _lowestTSugar; }
            set { _lowestTSugar = value; }
        }

        public DateTime DateLowestTSugar
        {
            get { return _dateLowestTSugar; }
            set { _dateLowestTSugar = value; }
        }

        public DateTime DateLowestRSugar
        {
            get { return _dateLowestRSugar; }
            set { _dateLowestRSugar = value; }
        }

        public double LowestRSugar
        {
            get { return _lowestRSugar; }
            set { _lowestRSugar = value; }
        }
        public DateTime DateLatestNicotine
        {
            get { return _dateLatestNicotine; }
            set { _dateLatestNicotine = value; }
        }

        public DateTime DateLatestTSugar
        {
            get { return _dateLatestTSugar; }
            set { _dateLatestTSugar = value; }
        }

        public DateTime DateLatestRSugar
        {
            get { return _dateLatestRSugar; }
            set { _dateLatestRSugar = value; }
        }

    }
}
