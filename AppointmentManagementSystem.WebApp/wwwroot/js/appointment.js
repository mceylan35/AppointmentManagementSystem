$(document).ready(function () {
    loadAppointments();

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

    // Randevular� y�kleme
    function loadAppointments() {
        $.ajax({
            url: '/Appointments',
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
