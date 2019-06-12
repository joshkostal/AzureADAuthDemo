import { AuthenticationContext } from '../../node_modules/adal-angular/lib/adal.js';

(function() {
    var authContext = new AuthenticationContext(config);
    let bearerToken = ''

    authContext.acquireToken(authContext.config.clientId, function(errorDesc, token, error) {
        if (error) { //acquire token failure
            console.log(errorDesc)

            if (config.popUp) {
                // If using popup flows
                authContext.acquireTokenPopup(authContext.config.clientId, null, null, function(errorDesc, token, error) {});
            } else {
                // In this case the callback passed in the Authentication request constructor will be called.
                authContext.acquireTokenRedirect(authContext.config.clientId, null, null);
            }
        } else {
            //acquired token successfully
            console.log(token)
            bearerToken = `Bearer ${token}`
        }
    })

    const blogUrl = 'https://localhost:44311/api/blog'

    function postBlog() {
        let title = document.getElementById('titleInput')
        let image = document.getElementById('imageInput')
        let body = document.getElementById('blogInput')

        let params = `title=${title.value}&image=${image.value}&body=${body.value}`

        let request = new XMLHttpRequest()
        request.open('POST', blogUrl, true)
        request.setRequestHeader('Content-type', 'application/x-www-form-urlencoded')
        request.setRequestHeader("Authorization", bearerToken)

        request.onreadystatechange = function() {
            if (request.readyState == 4 && request.status == 200) {
                if (request.response == 'success') {
                    title.value = ''
                    image.value = ''
                    body.value = ''
                } else {
                    console.log('error')
                }
            }
        }
        request.send(params)
    }

    document.getElementById('blogSubmit').addEventListener('click', function() {
        postBlog()
    })
}());