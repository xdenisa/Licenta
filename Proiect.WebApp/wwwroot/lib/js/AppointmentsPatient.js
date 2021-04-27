window.addEventListener("load", () => {
    let event = (() => {
        var source = document.getElementById("appointmentsPacient-template").innerHTML;
        var template = Handlebars.compile(source);
        let numberAppointments = 0;
        let idPatient = document.getElementById("idPatientInput").value;
        let canGet = true;
        return () => {
            if (canGet) {
                canGet = false;
                $.ajax({
                    type: 'GET',
                    url: "/Patient/GetAppointments",
                    data: {
                        toSkip: numberAppointments,
                        idPatient: idPatient
                    },
                    success: (result) => {
                        for (let i = 0; i < result.length; i++) {
                            let appointment = result[i];
                            appointment.appointmentDate = new Date(appointment.appointmentDate);
                            var html = template(appointment);
                            $("#appointmentsPacientBody").append(html);
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
                    },
                    error: (err) => {
                        console.error(err);
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

