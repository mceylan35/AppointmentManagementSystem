$(document).ready(function () {
    const createUserModal = new bootstrap.Modal(document.getElementById('createUserModal'));
    const editUserModal = new bootstrap.Modal(document.getElementById('editUserModal'));

    loadUsers();
    loadRoles();

    function loadUsers() {
        $.ajax({
            url: '/users/getusers',
            type: 'GET',
            success: function (response) {
                const tbody = $('#usersTable tbody');
                tbody.empty();
                if (!response.data || response.data.length === 0) {
                    tbody.append(`
                    <tr>
                        <td colspan="4" class="text-center">
                            <p class="text-muted my-3">Kullanıcı bulunamadı</p>
                        </td>
                    </tr>
                `);
                    return;
                }
                response.data.forEach(function (user) {
                    tbody.append(`
                        <tr>
                            <td>${user.username}</td>
                           
                             <td>${user.email}</td>
                            <td>${user.isActive ? 'Aktif' : 'Pasif'}</td>
                            <td>
                                <button class="btn btn-sm btn-primary edit-user" data-id="${user.id}">
                                    Düzenle
                                </button>
                                <button class="btn btn-sm btn-danger delete-user" data-id="${user.id}">
                                    Sil
                                </button>
                            </td>
                        </tr>
                    `);
                });
            },
             
            error: function (xhr) {
                const tbody = $('#usersTable tbody');
                tbody.empty();
                tbody.append(`
                <tr>
                    <td colspan="4" class="text-center">
                        <p class="text-danger my-3">
                            <i class="fas fa-exclamation-triangle me-2"></i>
                            Kullanıcılar yüklenirken bir hata oluştu
                        </p>
                    </td>
                </tr>
            `);
                toastr.error('Kullanıcılar yüklenirken bir hata oluştu: ' + (xhr.responseJSON?.message || 'Bilinmeyen hata'));
            } 

        });
    }

    function loadRoles() {
        $.ajax({
            url: '/roles',
            type: 'GET',
            success: function (roles) {
                const createSelect = $('#roles');
                const editSelect = $('#editRoles');

                roles.forEach(function (role) {
                    createSelect.append(`<option value="${role.id}">${role.name}</option>`);
                    editSelect.append(`<option value="${role.id}">${role.name}</option>`);
                });
            }
        });
    }

    // Yeni kullanıcı ekleme
    $('#saveUser').click(function () {
        const form = $('#createUserForm');

        if (!form[0].checkValidity()) {
            form[0].reportValidity();
            return;
        }

        const data = {
            username: $('#username').val(),
            email: $('#email').val(),
            password: $('#password').val(),
            roleIds: $('#roles').val()
        };

        $.ajax({
            url: '/users',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function (response) {
                createUserModal.hide();
                form[0].reset();
                loadUsers();
                if (response.successed)
                    toastr.success(response.message);
                else {
                    toastr.error(response.message);
                }
            },
            error: function (xhr) {
                toastr.error(xhr.responseJSON?.message || 'Kullanıcı oluşturulurken bir hata oluştu.');
            }
        });
    });

    // Kullanıcı düzenleme modalını aç
    $(document).on('click', '.edit-user', function () {
        const userId = $(this).data('id');

        $.ajax({
            url: `/users/${userId}`,
            type: 'GET',
            success: function (res) {
                var user = res.data;
                $('#editUserId').val(user.id);
                $('#editUsername').val(user.username);
                $('#editEmail').val(user.email);
                $('#editRoles').val(user.roleIds);
                $('#editIsActive').prop('checked', user.isActive);

                editUserModal.show();
            }
        });
    });

    // Kullanıcı güncelleme
    $('#updateUser').click(function () {
        const form = $('#editUserForm');

        if (!form[0].checkValidity()) {
            form[0].reportValidity();
            return;
        }

        const data = {
            id: $('#editUserId').val(),
            username: $('#editUsername').val(),
            email: $('#editEmail').val(),
            roleIds: $('#editRoles').val(),
            isActive: $('#editIsActive').is(':checked')
        };

        $.ajax({
            url: `/users/${data.id}`,
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function (response) {
                editUserModal.hide();
                form[0].reset();
                loadUsers();
                if (response.successed)
                    toastr.success(response.message);
                else {
                    toastr.error(response.message);
                }
                
            },
            error: function (xhr) {
                toastr.error(xhr.responseJSON?.message || 'Kullanıcı güncellenirken bir hata oluştu.');
            }
        });
    });

    // Kullanıcı silme
    $(document).on('click', '.delete-user', function () {
        const userId = $(this).data('id');

        if (confirm('Bu kullanıcıyı silmek istediğinizden emin misiniz?')) {
            $.ajax({
                url: `/users/${userId}`,
                type: 'DELETE',
                success: function (response) {
                    loadUsers();
                    if (response.successed)
                        toastr.success(response.message);
                    else {
                        toastr.error(response.message);
                    }
                },
                error: function (xhr) {
                    toastr.error('Kullanıcı silinirken bir hata oluştu.');
                }
            });
        }
    });
});