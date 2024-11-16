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
                toastr.success('Randevu ba�ar�yla olu�turuldu.');
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
                if (response.success) {
                    appointmentModal.hide(); // Bootstrap 5 modal kapatma
                    form[0].reset();
                    loadAppointments();
                    toastr.success('Randevu ba�ar�yla olu�turuldu.');
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
            success: function (appointment) {
                // Form alanlar�n� doldur
                $('#editAppointmentId').val(appointment.id);
                $('#editServiceId').val(appointment.serviceId);
                $('#editAppointmentDate').val(appointment.appointmentDate.slice(0, 16)); // datetime-local i�in format
                $('#editNotes').val(appointment.notes);

                // Modal� a�
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

        const data = {
            id: $('#editAppointmentId').val(),
            serviceId: $('#editServiceId').val(),
            appointmentDate: $('#editAppointmentDate').val(),
            notes: $('#editNotes').val()
        };
        const id = $('#editAppointmentId').val();
        $.ajax({
            url: `/appointment/${id}`,
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function (response) {
                if (response.success) {
                    editAppointmentModal.hide();
                    form[0].reset();
                    loadAppointments();
                    toastr.success('Randevu ba�ar�yla g�ncellendi.');
                } else {
                    toastr.error(response.message || 'Randevu g�ncellenirken bir hata olu�tu.');
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
            success: function (appointments) {
                const tbody = $('#appointmentsTable tbody');
                tbody.empty();

                appointments.forEach(function (appointment) {
                    tbody.append(`
                        <tr>
                            <td>${formatDate(appointment.appointmentDate)}</td>
                            <td>${appointment.serviceName}</td>
                            <td>${appointment.status}</td>
                            <td>
                                <button class="btn btn-sm btn-primary edit-appointment" 
                                        data-id="${appointment.id}">D�zenle</button>
                                <button class="btn btn-sm btn-danger delete-appointment" 
                                        data-id="${appointment.id}">�ptal</button>
                            </td>
                        </tr>
                    `);
                });
            }
        });
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
