import { AuthenticationContext } from '../../node_modules/adal-angular/lib/adal.js';

(function() {
    window.config = {
        clientId: 'your client id',
        tenant: 'your tenant id',
        postLogoutRedirectUri: 'https://localhost:8080/src/Views/index.html'
    };

    var authContext = new AuthenticationContext(config);

    // Check For & Handle Redirect From AAD After Login
    var isCallback = authContext.isCallback(window.location.hash);
    authContext.handleWindowCallback();

    if (isCallback && !authContext.getLoginError()) {
        window.location = authContext._getItem(authContext.CONSTANTS.STORAGE.LOGIN_REQUEST);
    }

    var user = authContext.getCachedUser();

    if (!user) {
        authContext.login();
    }

    function logout() {
        if (user) {
            authContext.logOut()
        }
    }

    document.getElementById('logout').addEventListener('click', function() {
        logout()
    })
}());