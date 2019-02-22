﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TwitterBots;

namespace BrawrdonCore.Controllers.Twitter
{
    [Route("/twitter/brawrdonbot")]
    [ApiController]
    public class TwitterController : ControllerBase
    {
        private readonly ITwitterBot _brawrdonBot;

        public TwitterController(BrawrdonBot brawrdonBot)
        {
            _brawrdonBot = brawrdonBot;
        }

        
        [HttpPost]
        public async Task<ActionResult> PostTweet([FromBody] Tweet tweetRequest)
        {
            HttpContext.Response.ContentType = "application/json";

            var tweetResponse = await _brawrdonBot.PostTweet(tweetRequest.Message);

            return tweetResponse.Value<int>("status") == 200 ? Ok(JObject.FromObject(new {tweetId = tweetResponse.Value<string>("tweetId")})) : new ObjectResult(JObject.FromObject(new { error = "There was an issue with starting BrawrdonBot." })) { StatusCode = 500 };
        }
    }
}
