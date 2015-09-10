﻿
using CliqueModel;
using Semantria.Com;
using Semantria.Com.Mapping;
using Semantria.Com.Mapping.Output;
using Semantria.Com.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueService.Business
{

    public class SemantriaBusiness
    {
        const string consumerKey = "2bbdf523-7842-4fce-b47a-d77c1315c41c";
        const string consumerSecret = "da7cacbe-98ac-4db8-8c59-c9e1fb48d8b7";


        public void GetScore(IEnumerable<SemantriaRequest> requestList)
        {
            // Creates JSON serializer instance
            ISerializer serializer = new JsonSerializer();

            // Initializes new session with the serializer object and the keys.
            using (Session session = Session.CreateSession(consumerKey, consumerSecret, serializer))
            {
                // Error callback handler. This event will occur in case of server-side error
                session.Error += new Session.ErrorHandler(delegate(object sender, ResponseErrorEventArgs ea)
                {
                    Console.WriteLine(string.Format("{0}: {1}", (int)ea.Status, ea.Message));
                });

                //Obtaining subscription object to get user limits applicable on server side
                Subscription subscription = session.GetSubscription();
                Dictionary<string, Semantria.Com.TaskStatus> docsTracker = new Dictionary<string, Semantria.Com.TaskStatus>();

                List<Document> outgoingBatch = new List<Document>(subscription.BasicSettings.BatchLimit);

                foreach (var item in requestList)
                {
                    if (docsTracker.ContainsKey(item.Guid))
                        continue;

                    docsTracker.Add(item.Guid, Semantria.Com.TaskStatus.QUEUED);

                    Document doc = new Document()
                    {
                        Id = item.Guid,
                        Text = item.Text
                    };

                    outgoingBatch.Add(doc);


                    if (outgoingBatch.Count == subscription.BasicSettings.BatchLimit)
                    {
                        break;
                    }

                }

                if (outgoingBatch.Count > 0)
                {
                    // Queues batch of documents for processing on Semantria service
                    if (session.QueueBatchOfDocuments(outgoingBatch) != -1)
                    {
                        Console.WriteLine(string.Format("{0} documents queued successfully.", outgoingBatch.Count));
                    }
                }

                List<DocAnalyticData> results = new List<DocAnalyticData>();
                int count = 0;
                while (docsTracker.Any(item => item.Value == Semantria.Com.TaskStatus.QUEUED) && count <= 100)
                {
                    count++;
                    System.Threading.Thread.Sleep(500);

                    // Requests processed results from Semantria service
                    Console.WriteLine("Retrieving your processed results...");
                    IList<DocAnalyticData> incomingBatch = session.GetProcessedDocuments();

                    foreach (DocAnalyticData item in incomingBatch)
                    {
                        if (docsTracker.ContainsKey(item.Id))
                        {
                            docsTracker[item.Id] = item.Status;
                            results.Add(item);
                        }
                    }
                }




                foreach (DocAnalyticData data in results)
                {
                    var currentRequest = requestList.First(res => res.Guid == data.Id);
                    float score = 0;
                    // Printing of document entities
                    if (data.Phrases != null && data.Phrases.Count > 0)
                    {

                        foreach (var entity in data.Phrases)
                        {
                            score += entity.SentimentScore;
                        }
                        score = score / data.Phrases.Count();
                    }

                    currentRequest.Score = score;


                }




            }
            

        }
    }
}
