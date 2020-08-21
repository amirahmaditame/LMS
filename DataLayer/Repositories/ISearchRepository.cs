﻿using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface ISearchRepository
    {
        IEnumerable<SearchCoursesListViewModel> searchCourses(string q);
        List<Categories> homeDetails();
        int courseCount();
        int teacherCount();
        int userCount();
       

    }
}
