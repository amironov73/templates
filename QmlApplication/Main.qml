import QtQuick 2.7
import QtQuick.Controls 2.0
import QtQuick.Layouts 1.15
import QtQuick.Dialogs 1.3

import app 1.0
 
ApplicationWindow {
    id: applicationWindow
    visible: true
    width: 340
    height: 280
    title: "Hello World"
    
    NetObject {
        id: model
    }
    
    MessageDialog {
        id: messageDialog
        icon: StandardIcon.Question
        title: "May I have your attention please"
        text: "It's so cool that you are using Qt Quick."
        onAccepted: {
            console.log("And of course you could only agree.")
            Qt.quit()
        }
    }
    
    ColumnLayout {
        anchors.fill: parent
        spacing: 6
        
        Text {
            Layout.fillWidth: true
            padding: 6
            wrapMode: Text.Wrap
            antialiasing: true 
            text: "У попа была собака, он её любил. Она съела кусок мяса, он её убил. В землю закопал и надпись написал."
            font.pixelSize: 14
        }
        
        Text {
            Layout.fillWidth: true
            padding: 6
            wrapMode: Text.Wrap
            horizontalAlignment: Text.Center
            antialiasing: true 
            text: model.someUsefulText
            font.pixelSize: 14
            color: "red"
        }
    
        Button {
            Layout.fillWidth: true
            text: "Нажми меня!"
            onClicked: model.handleClick()
        }
 
        Button {
            Layout.fillWidth: true
            text: "И меня!"
            onClicked: messageDialog.open()
        }
 
    }
}
