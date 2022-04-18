using Microsoft.EntityFrameworkCore;
using ToksApi.Data;
using ToksApi.Models;
using ToksApi.Models.ServiceModels;
using ToksApi.Models.UtilityClasses;

namespace ToksApi.Services
{
    public class ControllerServices : IControllerServices
    {
        private readonly IssueDbContext _issueDb;
        private readonly IUtilityServices _utilityServices;

        public ControllerServices(IssueDbContext issueDb, IUtilityServices utilityServices)
        {
            _issueDb = issueDb;
            _utilityServices = utilityServices;
        }

        #region Create

        //Create

        public async Task<BoolWithStringAndInt> CreateIssueAsync(IssueModifedModel issueModel)
        {
            var result = new BoolWithStringAndInt();
            string supplementalMessage = "";

            try
            {
                var issueData = new IssueModel()
                {
                    Description = issueModel.Description,
                    Title = issueModel.Title,
                    IssueType = issueModel.IssueType,
                    Priority = issueModel.Priority,
                    Created = DateTime.Now,
                };
                _issueDb.Issues.Add(issueData);
                await _issueDb.SaveChangesAsync();
                result.Bool = true;
                result.Value = "Issue Sucessfully Added";
                return result;


            }
            catch (Exception e)
            {

                result.Bool = false;
                result.Value = $"Issue UnSucessfully Added: Catch(Attempt) : {e.Message}";
                return result;
            }

            result.Bool = false;
            result.Value = $"Issue UnSucessfully Added: Try(Attempt) : {supplementalMessage}";
            return result;

        }

        #endregion

        #region Get

        // Get

        public async Task<IssueResultWithEnumerable> SearchIssueByIdAsync(uint Id)
        {
            var result = new IssueResultWithEnumerable();

            string supplementMessage = "";

            var issueList = new List<IssueModel>();

            try
            {

                if (!_issueDb.Issues.Any(x => x.Id == Id))
                {
                    result.Result = false;
                    result.Message = $"Failed in finding target Id";
                    return result;
                }
                var List = _issueDb.Issues.ToList();

                    foreach (var IssueList in List)
                    {

                        if (IssueList.Id == Id)
                        {

                            issueList.Add(new IssueModel
                            {
                                Id = IssueList.Id,
                                Title = IssueList.Title,
                                Description = IssueList.Description,
                                IssueType = IssueList.IssueType,
                                Priority = IssueList.Priority,
                                Completed = IssueList.Completed,
                                Created = IssueList.Created
                            });

                            result.Result = true;
                            result.issueModels = issueList;
                            result.Message = $"Succeded in finding target Id";
                            return result;

                        }

                    }              

            }
            catch (Exception e)
            {

                result.Result = false;
                result.Message = $"Failed Try Catch : {e.Message}";
                return result;
            }
            supplementMessage = "Failed entire thing";
            result.Result = false;
            result.Message = $"Failed Try Catch : {supplementMessage}";
            return result;

        }

        public async Task<IssueResultWithEnumerable> SearchIssueByTitleAsync(string SearchWord)
        {
            var result = new IssueResultWithEnumerable();

            var newIssueList = new List<IssueModel>();
            try
            {
                foreach (var List in _issueDb.Issues.ToList())
                {

                    var Searched = (await _utilityServices.SearchLongStringForSingleWordAsync(List.Title, SearchWord, List.Id));

                    if (Searched.Bool)
                    {
                        newIssueList.Add(new IssueModel
                        {
                            Id = List.Id,
                            Title = List.Title,
                            Description = List.Description,
                            IssueType = List.IssueType,
                            Priority = List.Priority,
                            Created = List.Created,
                            Completed = List.Completed
                        });
                        result.Id = (uint?)Searched.Int;
                        result.Result = true;
                        result.issueModels = newIssueList;
                        result.Message = "Found Issue With Title With a Word Matching The Searched";
                        return result;
                    }
 
                }
                result.Result = false;
                result.issueModels = newIssueList;
                result.Message = "Did Not Find Issue With Title With a Word Matching The Searched";
                return result;
            }
            catch (Exception e)
            {
                result.Result = false;
                result.Message = $"In Catch : {e.Message}";
                return result;

            }

            result.Result = false;
            result.Message = "Failed Try Catch";
            return result;

        }

        public async Task<IssueResultWithEnumerable> SearchIssueByDescriptionAsync(string SearchWord)
        {
            var result = new IssueResultWithEnumerable();

            var newIssueList = new List<IssueModel>();
            try
            {
                foreach (var List in _issueDb.Issues.ToList())
                {

                    var Searched = (await _utilityServices.SearchLongStringForSingleWordAsync(List.Description, SearchWord, List.Id));

                    if (Searched.Bool)
                    {
                        newIssueList.Add(new IssueModel
                        {
                            Id = List.Id,
                            Title = List.Title,
                            Description = List.Description,
                            IssueType = List.IssueType,
                            Priority = List.Priority,
                            Created = List.Created,
                            Completed = List.Completed
                        });

                        result.Id = (uint?)Searched.Int;
                        result.Result = true;
                        result.issueModels = newIssueList;
                        result.Message = "Found Issue With Title With a Word Matching The Searched";
                        return result;

                    }

                }
                result.Result = false;
                result.issueModels = newIssueList;
                result.Message = "Did Not Find Issue With Title With a Word Matching The Searched";
                return result;
            }
            catch (Exception e)
            {
                result.Result = false;
                result.Message = $"In Catch : {e.Message}";
                return result;

            }

            result.Result = false;
            result.Message = "Failed Try Catch";
            return result;
        }
        public async Task<IssueResultWithEnumerable> GetAllIssuesAsync()
        {
           
            var result = new IssueResultWithEnumerable();
            var list = _issueDb.Issues.ToList();
            var issueList = new List<IssueModel>();

            foreach (var item in list)
            {
                issueList.Add(new IssueModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    IssueType = item.IssueType,
                    Priority = item.Priority,
                    Created = item.Created,
                    Completed = item.Completed

                });
            }
            result.issueModels = issueList;
            return result;
        }


        public async Task<IssueResultWithEnumerable> SearchIssueByIssueTypeAsync(string? Target, IssueType? issueType)
        {
            var result = new IssueResultWithEnumerable();
            var list = new List<IssueModel>();
            var IssueList = _issueDb.Issues.ToList();

            try
            {
                foreach (var item in IssueList)
                {
                    list.Add(new IssueModel
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Description = item.Description,
                        IssueType = item.IssueType,
                        Priority = item.Priority,
                        Created = item.Created,
                        Completed = item.Completed
                    });
                    var match = _issueDb.Issues.Where(x => x.IssueType == issueType);
                    result.issueModels = match;

                    if (result.issueModels.Any())
                    {
                        result.Result = true;
                        return result;
                    }
                        result.Result = false;
                        result.Message = "IssueType Not In Database";
                        return result;


                }
            }
            catch (Exception e)
            {
                result.Result = false;
                result.Message = $"{e.Message}";
                return result;
            }

            result.Result = false;
            result.Message = "Failed Try Catch";
            return result;
        }

        public async Task<IssueResultWithEnumerable> SearchIssueByPriorityAsync(string? Target, Priority? priority)
        {
            var result = new IssueResultWithEnumerable();
            var list = new List<IssueModel>();
            var IssueList = _issueDb.Issues.ToList();

            try
            {
                foreach (var item in IssueList)
                {
                    list.Add(new IssueModel
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Description = item.Description,
                        IssueType = item.IssueType,
                        Priority = item.Priority,
                        Created = item.Created,
                        Completed = item.Completed
                    });
                    var match = _issueDb.Issues.Where(x => x.Priority == priority);
                    result.issueModels = match;

                    if (result.issueModels.Any())
                    {
                        result.Result = true;
                        return result;
                    }
                    result.Result = false;
                    result.Message = "IssueType Not In Database";
                    return result;                
                }

                    result.Result = false;
                    result.Message = "Priority Not Found";
                    return result;

                
            }
            catch (Exception e)
            {
                result.Result = false;
                result.Message = $"{e.Message}";
                return result;
            }

            result.Result = false;
            result.Message = "Failed Try Catch";
            return result;

        }

        #endregion

        #region Update

        //Update
        public async Task<BoolWithStringAndInt> UpdateIssueAsync(uint Id, IssueModifiedModelWithCompleted issueModel)
        {
            var result = new BoolWithStringAndInt();
            var list = await _issueDb.Issues.ToListAsync();

            try
            {

                foreach (var item in list)
                {
                    if (item.Id == Id)
                    {
                        _issueDb.Update(item);
                        item.Title = issueModel.Title;
                        item.Description = issueModel.Description;  
                        item.IssueType = issueModel.IssueType;
                        item.Priority = issueModel.Priority;
                        if (issueModel.IsCompleted)
                        {
                            item.Completed = DateTime.Now;
                        }
                        await _issueDb.SaveChangesAsync();

                        result.Bool = true;
                        result.Value = "Changes Done";
                        return result;
                    }
                }

                result.Bool = false;
                result.Value = "Changes Failed";
                return result;



            }
            catch (Exception e)
            {

                result.Bool = false;
                result.Value = $"{e.Message}";
                return result;
        
            }

            result.Bool = false;
            result.Value = "Try Catch Failed";
            return result;
 
        }
        public async Task<BoolWithStringAndInt> UpdateIssueCompltedAsync(uint Id, bool Completed)
        {
            var result = new BoolWithStringAndInt();
            var list = await _issueDb.Issues.ToListAsync();

            try
            {
                if (Completed)
                {
                    foreach (var item in list)
                    {
                        if (item.Completed == null)
                        {

                            if (item.Id == Id)
                            {
                                _issueDb.Update(item);
                                item.Completed = DateTime.Now;
                                await _issueDb.SaveChangesAsync();

                                result.Bool = true;
                                result.Value = "Changes Done";
                                return result;
                            }

                        }
                        result.Bool = false;
                        result.Value = "Already Completed";

                    }

                    return result;
                }

            }

            catch (Exception e)
            {

                result.Bool = false;
                result.Value = $"{e.Message}";
                return result;

            }

            result.Bool = false;
            result.Value = "Try Catch Failed";
            return result;
        }

        #endregion

        #region Delete

        //Delete
        public async Task<BoolWithStringAndInt> DeleteIssueAsync(uint Id)
        {
            var result = new BoolWithStringAndInt();
            var Issue = await _issueDb.Issues.FindAsync(Id);
            if (Issue != null)
            {
                _issueDb.Remove(Issue);
                await _issueDb.SaveChangesAsync();

                result.Bool = true;
                result.Value = $"Succed in Deleting : {Issue}";
                return result;
            }
            result.Bool = false;
            result.Value = $"Failed in Deleting {Id}";
            return result;
        }





        #endregion


    }
}
