using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRaschetZarplati
{
    public class Zadacha
    {
        public int id;
        public int executorID;
        public string title;
        public string description;
        public DateTime createDateTime;
        public DateTime deadline;
        public double difficulty;
        public int time;
        public string status;
        public string workType;
        public Nullable<DateTime> completedDateTime;
        public bool isDeleted;


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
        public int ExecutorID
        {
            get
            {
                return executorID;
            }
            set
            {
                executorID = value;
            }
        }
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        public DateTime CreateDateTime
        {
            get
            {
                return createDateTime;
            }
            set
            {
                createDateTime = value;
            }
        }
        public DateTime Deadline
        {
            get
            {
                return deadline;
            }
            set
            {
                deadline = value;
            }
        }
        public double Difficulty
        {
            get
            {
                return difficulty;
            }
            set
            {
                difficulty = value;
            }
        }
        public int Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
            }
        }
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }
        public string WorkType
        {
            get
            {
                return workType;
            }
            set
            {
                workType = value;
            }
        }
        public Nullable<DateTime> CompletedDateTime
        {
            get
            {
                return completedDateTime;
            }
            set
            {
                completedDateTime = value;
            }
        }
        public bool IsDeleted
        {
            get
            {
                return isDeleted;
            }
            set
            {
                isDeleted = value;
            }
        }

      

        public Zadacha(int id, int executorID, string title, string description,
            DateTime createDateTime, DateTime deadline, double difficulty, int time,
            string status, string workType, Nullable<DateTime> completedDateTime, bool isDeleted)
        {
            this.id = id;
            this.executorID = executorID;
            this.title = title;
            this.description = description;
            this.createDateTime = createDateTime;
            this.deadline = deadline;
            this.difficulty = difficulty;
            this.time = time;
            this.status = status;
            this.workType = workType;
            this.completedDateTime = completedDateTime;
            this.isDeleted = isDeleted;
        }
    }
}
