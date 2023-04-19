using Archery.Model;
using Archery.Model.ApiHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archery.Repository
{
    public class EventRepository : AbstractRepository
    {
        private readonly UserRepository _userRepository;
        private readonly TargetRepository _targetRepository;

        public EventRepository(ArcheryContext context, UserRepository userRepository, TargetRepository targetRepository) : base(context)
        {
            _userRepository = userRepository;
            _targetRepository = targetRepository;
        }

        public string StartEvent(NewEvent newEvent)
        {
            if (newEvent is null)
                throw new ArgumentNullException(nameof(newEvent), "Keine Eventdaten im Request!");

            List<int> allUserIds = new();
            foreach (var u in Context.User.ToArray())
                allUserIds.Add(u.Id);
            foreach (var userId in newEvent.UserIds)
                if (!allUserIds.Contains(userId))
                    throw new Exception($"User with Id {userId} does not exist!");

            var eventUser = Context.User
                .Where(u => newEvent.UserIds.Contains(u.Id))
                .ToList();

            var inactiveUser = _userRepository.GetAllInactiveUsers().ToList();
            var activeUser = Context.User.Where(u => !inactiveUser.Contains(u));

            foreach (var user in eventUser)
                if (activeUser.Contains(user))
                    throw new Exception($"User \"{user.NickName}\" is already participating in another Event!");

            var eventParcour = Context.Parcour
                .SingleOrDefault(u => newEvent.ParcourId == u.Id);

            if (eventParcour is null)
                throw new Exception("Parcour not found in Database!");

            var e = Context.Event.Add(new() { Name = newEvent.Name, Parcour = eventParcour, IsRunning = true }).Entity;

            foreach (var user in eventUser)
                Context.Mapping.Add(new() { Event = e, User = user });

            if (e == null)
                throw new Exception("Error adding the Event!");

            Context.SaveChanges();

            return e.Id.ToString();
        }


        public IEnumerable<AdminViewElement> GetAdminViewElements()
        {
            var userEventMappings = Context.Mapping
                .Include(m => m.User)
                .Include(m => m.Event)
                .Where(m => m.Event.IsRunning)
                .AsNoTracking()
                .ToArray();

            var mappings = Context.Mapping
                .Include(m => m.Target)
                .Include(m => m.Event)
                .Include(m => m.User)
                .Where(m => m.Event.IsRunning)
                .AsNoTracking()
                .ToArray();

            if (userEventMappings is null)
                throw new Exception();

            List<AdminViewElement> results = new();

            foreach (var uem in userEventMappings)
            {
                results.Add(new() { Id = uem.Event.Id, EventName = uem.Event.Name });

                var countingResults = new int[3, 3]{
                    {20, 18, 16},
                    {14, 12, 10},
                    {8, 6, 4},
                };

                foreach (var m in mappings.Where(m => m.Event.Id == uem.Event.Id && m.User?.Id == uem.User?.Id))
                {
                    var score = 0;
                    var targetsOfUser = m.Target;

                    foreach (var target in targetsOfUser)
                        if (target.ArrowCount != 0 && target.HitArea != 0)
                            score += countingResults[target.ArrowCount - 1, target.HitArea - 1];

                    results.Last().User.Add(new() { NickName = uem.User!.NickName, Score = score });
                }
            }

            List<AdminViewElement> groupedByEvents = new();

            var events = Context.Event
                .Include(e => e.Parcour)
                .Where(e => e.IsRunning)
                .AsNoTracking()
                .ToArray();

            foreach (var e in events)
            {
                List<AdminViewUser> user = new();

                var singleEventWithAllUsers = results.Where(r => r.EventName == e.Name);

                foreach (var ewu in singleEventWithAllUsers)
                    user.Add(new()
                    {
                        NickName = ewu.User[0].NickName,
                        Score = ewu.User[0].Score,
                        Targets = _targetRepository.GetMyTargets(Context.User.SingleOrDefault(u =>
                            u.NickName == ewu.User[0].NickName)?.Id
                            ?? throw new Exception("User not existing!"))
                    });

                var parcourName = e.Parcour.Name;

                groupedByEvents.Add(new() { Id = e.Id, EventName = e.Name, User = user, ParcourName = parcourName });
            }

            return groupedByEvents;
        }

        public int EndEvent(int eventToStop)
        {
            if (eventToStop <= 0)
                throw new InvalidOperationException("Invalid EventId");

            var stopEvent = Context.Event.SingleOrDefault(e => e.Id == eventToStop);

            if (stopEvent == null)
                throw new Exception("Fail: stopEvent ist Null");

            stopEvent.IsRunning = false;

            Context.SaveChanges();

            return stopEvent.Id;
        }
    }
}
