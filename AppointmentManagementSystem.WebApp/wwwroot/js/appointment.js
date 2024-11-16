$(document).ready(function () {
    loadAppointments();

    // Yeni randevu oluþturma
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
                toastr.success('Randevu baþarýyla oluþturuldu.');
            },
            error: function (xhr) {
                toastr.error('Randevu oluþturulurken bir hata oluþtu.');
            }
        });
    });

    // Randevularý yükleme
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
                                        data-id="${appointment.id}">Düzenle</button>
                                <button class="btn btn-sm btn-danger delete-appointment" 
                                        data-id="${appointment.id}">Ýptal</button>
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

        if (confirm('Randevuyu iptal etmek istediðinizden emin misiniz?')) {
            $.ajax({
                url: `/appointment/${id}`,
                type: 'DELETE',
                success: function () {
                    loadAppointments();
                    toastr.success('Randevu baþarýyla iptal edildi.');
                },
                error: function () {
                    toastr.error('Randevu iptal edilirken bir hata oluþtu.');
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
