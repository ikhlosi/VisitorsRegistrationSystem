﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.DTO;

namespace VisitorsRegistrationSystemBL.Interfaces
{
    public interface IVisitRepository
    {
        void AddVisit(Visit visit);
        void RemoveVisit(int id);
        void UpdateVisit(Visit visit);
        bool VisitExists(Visit visit);
        bool VisitExists(int visitorId, DateTime startTime);
        Visit GetVisit(int id);
        int EndVisit(string email, DateTime endTime);
        IReadOnlyList<VisitDTO> GetVisits();
        Visitor AddVisitor(Visitor visitor);
        void RemoveVisitor(int id);
        void UpdateVisitor(Visitor visitor);
        bool VisitorExists(Visitor visitor);
        bool VisitorExists(int id);
        Visitor GetVisitor(int id);
        List<Visitor> GetAllVisitors();
        bool VisitorExists(string email);
        Visitor GetVisitor(string email);
        IReadOnlyList<VisitDTO> GetVisitsByVisitorId(int visitordId);
    }
}
