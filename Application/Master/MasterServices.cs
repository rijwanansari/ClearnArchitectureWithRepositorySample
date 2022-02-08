using Application.Common.Interface;
using Application.Common.Mapping;
using Application.Common.Model;
using Application.Master.Dto;
using Domain.Master;
using Microsoft.EntityFrameworkCore;

namespace Application.Master
{
    internal class MasterServices : IMasterServices
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        #endregion
        #region Ctor
        public MasterServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
        //public async Task<ResponseModel> GetAllAppSettingAsync()
        //{
        //    try
        //    {
        //        var appsettigs = (await _unitOfWork.AppSettingRepo
        //            .TableNoTracking
        //            .OrderBy(t => t.ReferenceKey)
        //            .ToListAsync());
        //        return ResponseModel.SuccessResponse(GlobalDeclaration._successResponse, AppSettingVm.ConvertToView(appsettigs));
        //    }
        //    catch (Exception)
        //    {
        //        //handle exception
        //        return ResponseModel.FailureResponse(GlobalDeclaration._internalServerError);
        //    }
        //}

        #region Command
        public async Task<AppSetting> UpsertAsync(AppSetting appSetting)
        {
            try
            {
                if (appSetting.Id > 0)
                    _unitOfWork.AppSettingRepo.Update(appSetting);
                else
                    _unitOfWork.AppSettingRepo.Add(appSetting);

                await _unitOfWork.SaveAsync();
                return appSetting;
            }
            catch (Exception)
            {
                //Handle Exception
                throw;
            }

        }
        public async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                var IsDeteted = await _unitOfWork.AppSettingRepo.Delete(Id);
                await _unitOfWork.SaveAsync();
                return IsDeteted;
            }
            catch (Exception)
            {
                //handle exception
                return false;
            }
        }
        #endregion
        #region Queries
        public async Task<List<AppSetting>> GetAppSettingsAsync()
        {
            var appsettigs = await _unitOfWork.AppSettingRepo
                .TableNoTracking
                .OrderBy(t => t.ReferenceKey)
                .ToListAsync();
            return appsettigs;
        }
        public async Task<AppSetting> GetAppSettingByIdAsync(int Id)
        {
            try
            {
                var appSetting = await _unitOfWork.AppSettingRepo.Get(Id);
                return appSetting;
            }
            catch (Exception)
            {
                //handle exception
                throw;
            }
        }
        #endregion

    }
}
