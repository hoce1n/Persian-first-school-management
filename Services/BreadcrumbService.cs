using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace School.Services
{
    public class BreadcrumbService
    {
        private readonly NavigationManager _navigationManager;
        private readonly List<BreadcrumbItem> _breadcrumbItems = new();

        public BreadcrumbService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            _navigationManager.LocationChanged += (sender, args) => UpdateBreadcrumbs(args.Location);
        }

        public List<BreadcrumbItem> GetBreadcrumbs() => _breadcrumbItems;

        private void UpdateBreadcrumbs(string Uri)
        {
            var labelMap = new Dictionary<string, string>
        {
            { "/", "خانه"},
            { "/attendance", "حضور و غیاب"},
            { "/addteachers", "افزودن معلم"},
            { "/addclasses", "افزودن کلاس"},
            { "notifications", "اطلاعیه ها"},
            { "/send", "ارسال پیام"},
            { "/reports", "گزارشات" },
            { "/addstudents", "افزودن دانش‌آموز" },
            { "/access-denied", "دسترسی محدود"},
            { "/schoolinfo", "اطلاعات مدرسه"},
            { "/coming-soon", "به زودی..."}
        };

            var relativeUri = new Uri(Uri).AbsolutePath;

            if (!_breadcrumbItems.Any(item => item.Link == relativeUri))
            {
                var label = labelMap.ContainsKey(relativeUri) ? labelMap[relativeUri] : "4 0 4";
                _breadcrumbItems.Add(new BreadcrumbItem { Link = relativeUri, Label = label });
            }

            if (_breadcrumbItems.Count > 3)
            {
                _breadcrumbItems.RemoveAt(0);
            }

            BreadcrumbsChanged?.Invoke();
        }

        public void ClearBreadcrumbs()
        {
            _breadcrumbItems.Clear();
            BreadcrumbsChanged?.Invoke();
        }
        public event Action? BreadcrumbsChanged;
    };

    public class BreadcrumbItem
    {
        public string? Link { get; set; }
        public string? Label { get; set; }
    }
}