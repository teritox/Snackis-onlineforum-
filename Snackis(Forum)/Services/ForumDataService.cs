﻿using Snackis_Forum_.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Snackis_Forum_.Models;
using Microsoft.AspNetCore.Mvc;

namespace Snackis_Forum_.Services
{
    public class ForumDataService : IForumDataService
    {
        private readonly ForumContext _ctx;

        public ForumDataService(ForumContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<SiteTitle>> GetSiteTitle()
        {
            return await _ctx.SiteTitle.ToListAsync();
        }
        public async Task<IEnumerable<PrivateMessage>> GetPrivateMessages(string UserId)
        {
            return await _ctx.PrivateMessages.Where(u => u.ReceiverId == UserId).OrderByDescending(u => u.MessageSent).ToListAsync();
        }

        public PrivateMessage GetSinglePrivateMessage(int privateMessageId)
        {
            return _ctx.PrivateMessages.Where(p => p.Id == privateMessageId).FirstOrDefault();
        }
        public async Task<IEnumerable<Subject>> GetSubjects()
        {
            return await _ctx.Subjects.ToListAsync();
        }

        public async Task<IEnumerable<SubjectThread>> GetForumThreads()
        {
            return await _ctx.SubjectThreads.ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetForumPosts(int threadId)
        {
            return await _ctx.Posts.Where(p => p.ThreadId == threadId).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetReportedPosts()
        {
            return await _ctx.Posts.Where(p => p.Reported == true).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _ctx.Posts.ToListAsync();
        }

        public async Task<int> GetPostsCountInSubject(int subjectId)
        {
            var ThreadList = await GetForumThreads();
            var PostsList = await GetAllPosts();

            var count = 0;

            foreach (var thread in ThreadList.Where(t => t.SubjectId == subjectId))
            {
                count += PostsList.Where(p => p.ThreadId == thread.Id).Count();
            }

            return count;
        }

        public async Task<SubjectThread> GetLastestThreadInSubject(int subjectId)
        {
            var ThreadList = await GetForumThreads();

            var thread = ThreadList.Where(t => t.SubjectId == subjectId).OrderByDescending(t => t.CreationDate).FirstOrDefault();

            return thread;
        }
    }
}
