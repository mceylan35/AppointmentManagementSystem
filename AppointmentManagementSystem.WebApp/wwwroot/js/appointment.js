$(document).ready(function () {
    loadAppointments();
    loadServices();
    // Yeni randevu olu�turma
    $('#createAppointmentForm').on('submit', function (e) {
        e.preventDefault();

        const data = {
            appointmentDate: $('#appointmentDate').val(),
            serviceId: $('#serviceId').val(),
            notes: $('#notes').val()
        };

        $.ajax({
            url: '/appointment',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function (response) {
                $('#createAppointmentModal').modal('hide');
                loadAppointments();
                if (response.successed)
                    toastr.success(response.message);
                else {
                    toastr.error(response.message);
                }
            },
            error: function (xhr) {
                toastr.error('Randevu olu�turulurken bir hata olu�tu.');
            }
        });
    });

    $('#appointment-button').on('show.bs.modal', function () {
        const tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 1);
        $('#appointmentDate').attr('min', tomorrow.toISOString().split('T')[0]);
    });

    const appointmentModal = new bootstrap.Modal(document.getElementById('createAppointmentModal'));
    const editAppointmentModal = new bootstrap.Modal(document.getElementById('editAppointmentModal'));

    $('#saveAppointment').on('click', function () {
        const form = $('#createAppointmentForm');

        if (!form[0].checkValidity()) {
            form[0].reportValidity();
            return;
        }

        const data = {
            serviceId: $('#serviceId').val(),
            appointmentDate: $('#appointmentDate').val(),
            notes: $('#notes').val()
        };

        $.ajax({
            url: '/Appointment/Create',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function (response) {
                if (response.successed) {
                    appointmentModal.hide();  
                    form[0].reset();
                    loadAppointments();
                  
                   toastr.success(response.message);
                    
                } else {
                    toastr.error(response.message || 'Randevu olu�turulurken bir hata olu�tu.');
                }
            },
            error: function (xhr) {
                toastr.error('Randevu olu�turulurken bir hata olu�tu.');
            }
        });
    });

    document.getElementById('createAppointmentModal').addEventListener('show.bs.modal', function () {
        const tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 1);
        $('#appointmentDate').attr('min', tomorrow.toISOString().split('T')[0]);
    });


    $(document).on('click', '.edit-appointment', function () {
        const appointmentId = $(this).data('id');

        // Randevu bilgilerini getir
        $.ajax({
            url: `/Appointment/${appointmentId}`,
            type: 'GET',
            success: function (response) {
                const appointment = response.data;
                $('#editAppointmentId').val(appointment.id);

              
                    $('#editCustomerInfo').val(`${appointment.userName}`);
                    $('#editServiceInfo').val(appointment.serviceName);
                    $('#editServiceId').val(appointment.serviceId); // ServiceId'yi sakla
                    $('#editDateInfo').val(formatDate(appointment.appointmentDate));
                  $('#editStatus').val(getStatusTextToInt(appointment.status));
                    
                   
                $('#editAppointmentDate').val(appointment.appointmentDate);
               
               
                $('#editNotes').val(appointment.notes);

                editAppointmentModal.show();
            },
            error: function () {
                toastr.error('Randevu bilgileri al�n�rken bir hata olu�tu.');
            }
        });
    });

    // G�ncelle butonuna t�kland���nda
    $('#updateAppointment').on('click', function () {
        const form = $('#editAppointmentForm');

        if (!form[0].checkValidity()) {
            form[0].reportValidity();
            return;
        }
        const statusValue = $('#editStatus').val();
        const data = {
            id: $('#editAppointmentId').val(),
            serviceId: $('#editServiceId').val(),
            appointmentDate: $('#editAppointmentDate').val(),
            notes: $('#editNotes').val(), 
            status: Number.isNaN(Number.parseInt(statusValue)) ? 0 : Number.parseInt(statusValue) 
        }; 

        const id = $('#editAppointmentId').val();
        $.ajax({
            url: `/appointment/${id}`,
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function (response) {
                if (response.successed) {
                    editAppointmentModal.hide();
                    form[0].reset();
                    loadAppointments();
                    toastr.success(response.message);
                } else {
                    toastr.error(response.message);
                }
            },
            error: function (xhr) {
                toastr.error('Randevu g�ncellenirken bir hata olu�tu.');
            }
        });
    });

    // Edit modal a��ld���nda servisleri y�kle
    document.getElementById('editAppointmentModal').addEventListener('show.bs.modal', function () {
        // Servisleri y�kle (e�er zaten y�klenmemi�se)
        if ($('#editServiceId option').length <= 1) {
            $.ajax({
                url: '/Services',
                type: 'GET',
                success: function (services) {
                    const select = $('#editServiceId');
                    select.empty().append('<option value="">Hizmet Se�iniz</option>');

                    services.forEach(function (service) {
                        select.append(`<option value="${service.id}" 
                         >
                        ${service.name}  
                    </option>`);
                    });
                },
                error: function () {
                    toastr.error('Hizmetler y�klenirken bir hata olu�tu.');
                }
            });
        }

        // Minimum tarih kontrol�
        const tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 1);
        $('#editAppointmentDate').attr('min', tomorrow.toISOString().split('T')[0]);
    });











    // Randevular� y�kleme
    function loadAppointments() {
        $.ajax({
            url: '/Appointment/appointments',
            type: 'GET',
            success: function (response) {
                const tbody = $('#appointmentsTable tbody');
                tbody.empty();

                response.data.forEach(function (appointment) {
                    let row = `
                    <tr>
                        <td>${formatDate(appointment.appointmentDate)}</td>
                        ${isAdmin ? `<td>${appointment.userName}</td>` : ''}
                        <td>${appointment.serviceName}</td>
                        <td>
                            <span class="badge ${getStatusBadgeClass(appointment.status)}">
                                ${getStatusText(appointment.status)}
                            </span>
                        </td>
                      
                        <td>${getActionButtons(appointment)}</td>
                    </tr>
                `;
                    tbody.append(row);
                });
            }
        });
    }


    function getActionButtons(appointment) {
        if (isAdmin) {
            return `
            <button class="btn btn-sm btn-primary edit-appointment" data-id="${appointment.id}">
                Durum Guncelle
            </button>`;
        } else {
            // Sadece Pending durumundaki randevular i�in d�zenleme ve iptal
            if (appointment.status !== 0) { // Pending
                return `
                <button class="btn btn-sm btn-primary edit-appointment" data-id="${appointment.id}">
                    D�zenle
                </button>
                <button class="btn btn-sm btn-danger delete-appointment" data-id="${appointment.id}">
                    Sil
                </button>`;
            } 
        }
    }

    function getStatusTextToInt(status) {
        switch (status) {
            case 'Pending': return 0;
            case 'Confirmed': return 1;
            case 'Cancelled': return 2;
            case 'Completed': return 3;
            default: return 0;
        }
    }
    function getStatusText(status) {
        switch (status) {
            case 'Pending': return 'Beklemede';
            case 'Confirmed': return 'Onaylandi';
            case 'Cancelled': return 'Iptal Edildi';
            case 'Completed': return 'Tamamlandi';
            default: return 'Bilinmiyor';
        }
    }

    function getStatusBadgeClass(status) {
        switch (status) {
            case 'Pending': return 'bg-warning';   
            case 'Confirmed': return 'bg-success';   
            case 'Cancelled': return 'bg-danger';    
            case 'Completed': return 'bg-info';      
            default: return 'bg-secondary';
        }
    }


    function loadServices() {
        $.ajax({
            url: '/Services',
            type: 'GET',
            success: function (services) {
                const select = $('#serviceId');
                select.empty().append('<option value="">Hizmet Se�iniz</option>');

                services.forEach(function (service) {
                    select.append(`<option value="${service.id}" 
                        data-duration="${service.duration}">
                        ${service.name} (${service.duration} dk)
                    </option>`);
                });
            },
            error: function () {
                toastr.error('Hizmetler y�klenirken bir hata olu�tu.');
            }
        });
    }
    // Randevu silme
    $(document).on('click', '.delete-appointment', function () {
        const id = $(this).data('id');

        if (confirm('Randevuyu iptal etmek istedi�inizden emin misiniz?')) {
            $.ajax({
                url: `/appointment/${id}`,
                type: 'DELETE',
                success: function () {
                    loadAppointments();
                    toastr.success('Randevu ba�ar�yla iptal edildi.');
                },
                error: function () {
                    toastr.error('Randevu iptal edilirken bir hata olu�tu.');
                }
            });
        }
    });

    // Tarih formatlama
    function formatDate(date) {
        return new Date(date).toLocaleDateString('tr-TR', {
            day: '2-digit',
            month: '2-digit',
            year: 'numeric',
            hour: '2-digit',
            minute: '2-digit'
        });
    }
});
