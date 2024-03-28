using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PuyuanDotNet8.Dtos;

namespace PuyuanDotNet8.Services
{
    public class DrugServices
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        JsonResult success = new JsonResult(new { status = "0" });
        JsonResult fail = new JsonResult(new { status = "1" });
        public DrugServices(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Druginfoget(DrugDto drugget,string uuid)
        {
            var user = _context.DrugInformation.Where(h => h.Uuid == uuid && h.Drug_Type == drugget.type);
            if (user == null)
            {
                return fail;
            }
            var response = new
            {
                status = "0",
                a1cs = user.Select(user => new
                {
                    id=user.Id,
                    user_id=user.Uuid,
                    type=user.Drug_Type,
                    name=user.Name,
                    recorded_at=user.Recorded_At
                })
            };

            JsonResult success = new JsonResult(response);
            return success;
        }
        public async Task<IActionResult> DruginfoUpload(DrugUploadDto drugUpload,string uuid)
        {
            var user = _context.DrugInformation.FirstOrDefault(h => h.Uuid == uuid);
            DrugInformation _druginfo = new DrugInformation()
            {
                Uuid = uuid,
                Drug_Type= drugUpload.Type,
                Name = drugUpload.Name,
                Recorded_At= drugUpload.recorded_at
            };
            _druginfo.Created_At = DateTime.Now;
            _druginfo.Updated_At = DateTime.Now;
            _context.DrugInformation.Add(_druginfo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return fail;
            }
            return success;
        }

        public async Task<IActionResult> DrugInfoDelete(DrugDeleteDto drugDelete,string uuid)
        {
            var matchedRecords = _context.DrugInformation.Where(h => h.Uuid == uuid).OrderBy(h => h.Id).ToList(); // 假设按 Id 排序
            var indexesToDelete = drugDelete.ids.Distinct().OrderBy(x => x).ToList(); // 去重并排序
            var recordsToDelete = new List<DrugInformation>(); // 用你的实体类型替换 YourEntityType
            foreach (var index in indexesToDelete)
            {
                if (index >= 0 && index < matchedRecords.Count) // 确保索引有效
                {
                    recordsToDelete.Add(matchedRecords[index]);
                }
            }
            if (!recordsToDelete.Any())
            {
                return fail;
            }
            _context.DrugInformation.RemoveRange(recordsToDelete);
            await _context.SaveChangesAsync();
            return success;
        }
    }
}
