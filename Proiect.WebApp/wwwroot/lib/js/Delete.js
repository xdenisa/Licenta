function reloadPage() {
    location.reload();
}

function deleteProfile() {
    let idMedic = document.getElementById("idMedicInput").value;
    var okay = confirm("Sunteți sigur(ă) că doriți ștergerea contului?");
    if (okay) {
        $.ajax({
            type: "DELETE",
            url: `/Medic/DeleteProfile`,
            data: {
                idMedic: idMedic
            },
            success: reloadPage,
            error: console.log
        });
    }
}

function deletePatient() {
    let idPatient = $("#btnDeletePatient").data("id");
    var okay = confirm("Sunteți sigur(ă) că doriți ștergerea contului?");
    if (okay) {
        $.ajax({
            type: "DELETE",
            url: `/Admin/DeletePatient`,
            data: {
                idPatient: idPatient
            },
            success: reloadPage,
            error: console.log
        });
    }
}

function deleteMedic() {
    let idMedic = $("#btnDeleteMedic").data("id");
    var okay = confirm("Sunteți sigur(ă) că doriți ștergerea contului?");
    if (okay) {
        $.ajax({
            type: "DELETE",
            url: `/Admin/DeleteMedic`,
            data: {
                idMedic: idMedic
            },
            success: reloadPage,
            error: console.log
        });
    }
}

function deletePatientProfile() {
    let idPatient = document.getElementById("idPatientInput").value;
    var okay = confirm("Sunteți sigur(ă) că doriți ștergerea contului?");
    if (okay) {
        $.ajax({
            type: "DELETE",
            url: `/Patient/DeleteProfile`,
            data: {
                idPatient: idPatient
            },
            success: reloadPage,
            error: console.log
        });
    }
}


function deleteAppointment() {
    let idMedic = $("#btnDeleteAppointment").data("medic");
    let id = $("#btnDeleteAppointment").data("id");
    var okay = confirm("Sunteți sigur(ă) că doriți să refuzați programarea?");
    if (okay) {
        $.ajax({
            type: "DELETE",
            url: `/Medic/DeleteAppointment`,
            data: {
                idMedic: idMedic,
                idAppointment: id

            },
            success: reloadPage,
            error: console.log
        });
    }
}

function deletePatientAppointment() {
    let idPatient = $("#btnDeletePatientAppointment").data("patient");
    let id = $("#btnDeletePatientAppointment").data("id");
    var okay = confirm("Sunteți sigur(ă) că doriți să anulați programarea?");
    if (okay) {
        $.ajax({
            type: "DELETE",
            url: `/Patient/DeleteAppointment`,
            data: {
                idPatient: idPatient,
                idAppointment: id

            },
            success: reloadPage,
            error: console.log
        });
    }
}

function deleteMedicine() {
    let idPatient = $("#btnDeleteMedicine").data("patient");
    let id = $("#btnDeleteMedicine").data("id");
    var okay = confirm("Sunteți sigur(ă) că doriți să întrerupeți medicația?");
    if (okay) {
        $.ajax({
            type: "DELETE",
            url: `/Patient/DeleteMedicine`,
            data: {
                idPatient: idPatient,
                idTreatment: id

            },
            success: reloadPage,
            error: console.log
        });
    }
}

function deletePortfolio() {
    let idPatient = $("#btnDeletePortfolio").data("patient");
    let id = $("#btnDeletePortfolio").data("id");
    var okay = confirm("Sunteți sigur(ă) că doriți să ștergeți documentul?");
    if (okay) {
        $.ajax({
            type: "DELETE",
            url: `/Patient/DeletePortfolio`,
            data: {
                idPatient: idPatient,
                idResult: id

            },
            success: reloadPage,
            error: console.log
        });
    }
}