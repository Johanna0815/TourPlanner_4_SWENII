﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner_4_SWENII.Models;

namespace TourPlanner_4_SWENII.DAL
{
    public class DataHandlerEF : IDataHandler
    {
        private TourPlannerDBContext context = new();

        public DataHandlerEF()
        {
            context.Database.EnsureCreated();
        }

        public Tour AddTour(Tour newTour)
        {
            context.Tours.Add(newTour);
            context.SaveChanges();
            return newTour;
        }

        public TourLog AddTourLog(TourLog newTourLog)
        {
            //change to use tourlogs directly??
            context.Tours.Where(t => t.Id == newTourLog.TourId).First().TourLogs.Add(newTourLog);
              
            context.SaveChanges();
            return newTourLog;
        }

        public void DeleteTour(Tour tour)
        {
            context.Tours.Remove(tour);
            context.SaveChanges();
        }

        public void DeleteTourLog(TourLog tourLog)
        {
            context.TourLogs.Remove(tourLog);
            context.SaveChanges();
        }

        public TourLog UpdateTourLog(TourLog tourlog)
        {
            var entity = context.TourLogs.Find(tourlog.Id);
            if (entity == null)
            {
                throw new Exception("TourLog could not be edited as DataHandlerEF.EditTourLogs() didn't find a valid TourLog with the same Id");
            }

            context.Entry(entity).CurrentValues.SetValues(tourlog);
            context.SaveChanges();
            return tourlog;
        }

        public IEnumerable<TourLog> GetTourLogs(int tourId)
        {
            if (context.TourLogs.Where(t => t.TourId == tourId).Count() > 0)
            {
                return context.TourLogs.Where(t => t.TourId == tourId);
            }
            else
            {
                Debug.WriteLine($"No Tour with id {tourId} found");
                return new List<TourLog>();
            }
        }

       public IEnumerable<Tour> GetTours()
        {
            return context.Tours;
        }

        public Tour UpdateTour(Tour newTour)
        {
            context.Tours.Update(newTour);
            context.SaveChanges();
            return newTour;
        }

    }
}
