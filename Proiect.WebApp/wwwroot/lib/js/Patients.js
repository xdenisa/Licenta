window.addEventListener("load", () => {
    let event = (() => {
        var source = document.getElementById("patients-template").innerHTML;
        var template = Handlebars.compile(source);
        let numberPatients = 0;
        let idMedic = document.getElementById("idMedicInput").value;
        let canGet = true;
        return () => {
            if (canGet) {
                canGet = false;
                $.ajax({
                    type: 'GET',
                    url: "/Medic/GetPatients",
                    data: {
                        toSkip: numberPatients,
                        idMedic: idMedic
                    },
                    success: (result) => {
                        for (let i = 0; i < result.length; i++) {
                            let patient = result[i];
                            patient.appointmentDate = new Date(patient.appointmentDate);
                            var html = template(patient);
                            $("#patientsBody").append(html);
                            $(".modalButton").click(function () {
                                let id = $(this).data("id");
                                $(`#${id}`).modal();
                            });
                        }
                        numberPatients += result.length;
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
        if ((window.innerHeight + window.scrollY) > document.body.offsetHeight-150) {
            event();
        }
    });

});

