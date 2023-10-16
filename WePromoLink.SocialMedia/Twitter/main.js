const { TwitterApi } = require('twitter-api-v2');

const userClient = new TwitterApi({
    appKey: 'NctjDHIwZWPXAGwwGbTpXt5Jf',
    appSecret: 'j45qeEyBzPAJ5G6VXE5UTfqF1BusWIc5f9chV7lVVFHn4htW3t',
    // Following access tokens are not required if you are
    // at part 1 of user-auth process (ask for a request token)
    // or if you want a app-only client (see below)
    accessToken: '1652540878894792704-6RRtYwjapueZ3FNLE6Bb2hfKFcnOv6',
    accessSecret: 'iRhDsjlGmxQea2eOyDLMPPzvS3dYmdgQ3w4jzB7Gw8mXG',
  });
  
  userClient.v2.tweet('Hello from bot')
  .then(()=>console.log('done'))
  .catch(e=> console.error(e));
  // You can upload media easily!
//   await twitterClient.v1.uploadMedia('./big-buck-bunny.mp4');