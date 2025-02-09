﻿using Snackis_Forum_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis_Forum_.Services
{
    public interface IForumDataService
    {
        Task <IEnumerable<SiteTitle>> GetSiteTitle();
        Task<IEnumerable<PrivateMessage>> GetPrivateMessages(string UserId);
        PrivateMessage GetSinglePrivateMessage(int privateMessageId);
        Task<IEnumerable<Subject>> GetSubjects();
        Task<IEnumerable<SubjectThread>> GetForumThreads();
        Task<IEnumerable<Post>> GetForumPosts(int threadId);
        Task<IEnumerable<Post>> GetReportedPosts();
        Task<IEnumerable<Post>> GetAllPosts();
        Task<int> GetPostsCountInSubject(int subjectId);
        Task<SubjectThread> GetLastestThreadInSubject(int subjectId);
    }
}
