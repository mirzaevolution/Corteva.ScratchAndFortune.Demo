var segmentsArray = [];
var zonkCode = "00000000-0000-0000-0000-000000000000";
var usedCode = "11111111-1111-1111-1111-111111111111";
var theWheel = undefined;
var message = "";
var win = false;
function drawTriangle() {
    // Get the canvas context the wheel uses.
    let ctx = theWheel.ctx;

    ctx.strokeStyle = 'black';          // Set line colour.
    ctx.fillStyle = 'lime';             // Set fill colour.
    ctx.lineWidth = 2;
    ctx.beginPath();                    // Begin path.
    ctx.moveTo(130, 5);                 // Move to initial position.
    ctx.lineTo(190, 5);                 // Draw lines to make the shape.
    ctx.lineTo(160, 40);
    ctx.lineTo(131, 5);
    ctx.stroke();                       // Complete the path by stroking (draw lines).
    ctx.fill();                         // Then fill.
}
function alertPrize() {
    if (win) {
        swal("Menang", message, "info").then(function(value) {
            location.href = "https://www.corteva.id/solusi-benih-hibrida-dan-pengendalian-hama-gulma-penyakit.html";
        });;
    } else {
        swal("Gagal", message, "info");
    }
}
var initWinwheel = {
    init: function() {
        $("#BtnCheckReward").click(function() {
            initWinwheel.checkReward();
        });
        this.callGetRewards();
    },
    winWheel: function(listOfRewards) {
        var segments = [];
        $.each(listOfRewards, function(index, value) {
            segments.push({ fillStyle: value.color, text: value.name });
        });
        theWheel = new Winwheel({
            canvasId: "spinner",
            innerRadius: 20,
            numSegments: 8,
            outerRadius: 140,
            segments: segments,
            textFillStyle: "white",
            textFontSize: 18,
            textDirection: "reversed",
            animation:
            {
                type: 'spinToStop',
                duration: 5,
                spins: 8,
                callbackAfter: 'drawTriangle()',
                callbackFinished: 'alertPrize()'
            }
        });

        // Usual pointer drawing code.
        drawTriangle();

      
    },
    checkReward: function() {
        var url = "/Spinner/CheckReward";
        $.ajax({
            url: url,
            method: "POST",
            data: {
                voucher: $("#DecodedQ").val()
            },
            success: function(response) {
                if (response.success) {
                    if (response.rewardCode === zonkCode) {
                        message = "Mohon maaf anda gagal memenangkan undian ini";
                        win = false;
                        var segment = segmentsArray.find(function(v, i) {
                            return v.id === response.rewardCode;
                        });

                        var segmentIndex = segment.segmentIndex;
                        var stopAt = theWheel.getRandomForSegment(segmentIndex);

                        // Important thing is to set the stopAngle of the animation before stating the spin.
                        theWheel.animation.stopAngle = stopAt;

                        // Start the spin animation here.
                        theWheel.startAnimation();

                    } else if (response.rewardCode === usedCode) {
                        swal("Kesalahan", "Mohon maaf voucher sudah pernah digunakan sebelumnya", "error");

                    } else {
                        var segment = segmentsArray.find(function(v, i) {
                            return v.id === response.rewardCode;
                        });
                        var segmentIndex = segment.segmentIndex;
                        var rewardName = segment.name;
                        var stopAt = theWheel.getRandomForSegment(segmentIndex);

                        message = "Selamat, anda mendapatkan hadiah " + rewardName + ". Corteva akan segera menghubungi anda melalui nomor telepon yang sudah didaftarkan";
                        win = true;
                        // Important thing is to set the stopAngle of the animation before stating the spin.
                        theWheel.animation.stopAngle = stopAt;

                        // Start the spin animation here.
                        theWheel.startAnimation();
                    }

                } else {
                    swal("Error", "Terjadi kesalahan server, mohon refresh page ini kembali", "error");
                }
            },
            error: function() {
                swal("Error", "Terjadi masalah koneksi internet", "error");
            }
        });
    },
    callGetRewards: function() {
        var url = "/Spinner/GetRewards";
        $.ajax({
            url: url,
            method: "POST",
            success: function(response) {
                segmentsArray = [];
                var iterator = 1;
                $.each(response.rewards, function(index, value) {
                    value.segmentIndex = iterator++;
                    segmentsArray.push(value);
                });
                initWinwheel.winWheel(segmentsArray);
            }
        });
    }
};

$(document).ready(function() {
    initWinwheel.init();
});