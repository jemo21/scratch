// Copyright (c) 2019 Google LLC.
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not
// use this file except in compliance with the License. You may obtain a copy of
// the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations under
// the License.

using Google.Apis.Auth.OAuth2;
using Google.Apis.CloudResourceManager.v1;
using Google.Apis.Services;
using System;

namespace ListProjects
{
    class Program
    {
        static void Main(string[] args)
        {
 
             GoogleCredential credential =
                GoogleCredential.GetApplicationDefault();
            if (credential.IsCreateScopedRequired)
            {
                credential = credential.CreateScoped(new[]
                {
                    CloudResourceManagerService.Scope.CloudPlatformReadOnly
                });
            }
            var crmService = new CloudResourceManagerService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
            });
            var request = new ProjectsResource.ListRequest(crmService);
            while (true)
            {
                var result = request.Execute();
                foreach (var project in result.Projects)
                {
                    Console.WriteLine(project.ProjectId);
                }
                if (string.IsNullOrEmpty(result.NextPageToken))
                {
                    break;
                }
                request.PageToken = result.NextPageToken;
            }
        }
    }
}
