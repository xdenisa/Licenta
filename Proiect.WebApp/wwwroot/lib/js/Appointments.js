window.addEventListener("load", () => {
    let event = (() => {
        var source = document.getElementById("appointments-template").innerHTML;
        var template = Handlebars.compile(source);
        let numberAppointments = 0;
        let idMedic = document.getElementById("idMedicAppointments").value;
        let canGet = true;
        return () => {
            if (canGet) {
                canGet = false;
                $.ajax({
                    type: 'GET',
                    url: "/Medic/GetAppointments",
                    data: {
                        toSkip: numberAppointments,
                        idMedic: idMedic
                    },
                    success: (result) => {

                        for (let i = 0; i < result.length; i++) {
                            let appointment = result[i];
                            appointment.appointmentDate = new Date(appointment.appointmentDate);
                            var html = template(appointment);
                            $("#appointmentsBody").append(html);
                            $(".modalButton").click(function () {
                                let id = $(this).data("id");
                                $(`#${id}`).modal();
                            });
                        }
                        numberAppointments += result.length;
                        canGet = true;
                        if (result.length === 0) {
                            canGet = false;
                        }
                    }
                })
            }
        }
    })();
    event();
    $(window).scroll(() => {
        if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight - 150) {
            event();
        }
    });

});
