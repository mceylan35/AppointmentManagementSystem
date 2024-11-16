function getToken() {
    return localStorage.getItem('jwt_token');
}
$.ajaxSetup({
    beforeSend: function (xhr) {
        const token = getToken();
        if (token) {
            xhr.setRequestHeader('Authorization', `Bearer ${token}`);
        }
    }
});