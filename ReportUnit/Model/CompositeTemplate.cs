using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RazorEngine;
using RazorEngine.Templating;
using RazorEngine.Text;

namespace ReportUnit.Model
{
    public class CompositeTemplate
    {
        private List<Report> _reportList;

        public void AddReport(Report report)
        {
            if (_reportList == null)
            {
                _reportList = new List<Report>();
            }

            _reportList.Add(report);

            SideNavLinks += Engine.Razor.RunCompile(Templates.SideNav.Link, "sidenav", typeof(Report), report, null);
        }

        public List<TestInfo> TestInfo
        {
            get
            {
                _mergedTests = _reportList.First().TestSuiteList.
                        SelectMany(ts => ts.TestList,
                        (testSuite, test) => new TestInfo()
                        {
                            Name = test.Name,
                            FullName = test.FullName,
                            Fixture = testSuite.Name,
                            Description = test.Description,
                            Statuses = new List<StatusInfo>()
                            {
                                new StatusInfo()
                                {
                                    Status = test.Status.ToString(),
                                    Message = test.StatusMessage
                                }
                            }
                        }).ToList();
                for (int i = 1; i < _reportList.Count; i++)
                {
                    var list = _reportList[i].TestSuiteList.SelectMany(ts => ts.TestList,
                        (testSuite, test) => new
                        {
                            Name = test.Name,
                            FullName = test.FullName,
                            Fixture = testSuite.Name,
                            Status = test.Status.ToString(),
                            Message = test.StatusMessage
                        });
                    foreach (var test in _mergedTests)
                    {
                        var item = list.FirstOrDefault(l => l.FullName == test.FullName && l.Fixture == test.Fixture);
                        if (item != null)
                        {
                            test.Statuses.Add(new StatusInfo()
                            {
                                Status = item.Status,
                                Message = item.Message
                            });
                        }
                    }
                }

                return _mergedTests.OrderBy(test => test.Statuses.First().Status).ToList();
            }
        }

        private List<TestInfo> _mergedTests { get; set; }

        public List<Report> ReportList
        {
            get
            {
                return _reportList;
            }
        }

        public TimeSpan OverallTestRunDuration
        {
            get
            {
                //return new TimeSpan(TestSuiteList.Sum(ts => ts.DurationTime.Ticks));
                return new TimeSpan(ReportList.Sum(r => r.DurationTime.Ticks));
            }
        }

        public string SideNavLinks { get; internal set; }
    }

    public class TestInfo
    {
        public string Fixture { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public List<StatusInfo> Statuses { get; set; }

        public TestInfo()
        {
            Statuses = new List<StatusInfo>();
        }

    }

    public class StatusInfo
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
