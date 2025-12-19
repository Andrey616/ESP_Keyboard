#include <BluetoothSerial.h>
#include <BleKeyboard.h>
#include <ArduinoJson.h>
#include "esp_bt_main.h"
#include "esp_bt_device.h"

#define LED_PIN 2
struct StructTimeAndFlag {int key; unsigned long TimeMillisBut; bool LetGoFlagBut;};
const int ButtonPins[] = {4, 16, 17, 5, 18, 19};
const int ButtonCount = sizeof(ButtonPins) / sizeof(ButtonPins[0]);
StructTimeAndFlag TimeAndFlag [ButtonCount];


struct StructMakrosKey {
    int key; 
    char combination[4];
};
StructMakrosKey MakrosKey[] = {
    {0, {0x80,99,0,0}},
    {1, {0x80,118,0,0}},
    {2, {0x80,98,0,0}},
    {3, {0,0,0,0}},
    {4, {0,0,0,0}},
    {5, {0xD4,0,0,0}}
};
BleKeyboard bleKeyboard("ESP32-Keyboard", "Arduino", 100); // создаем объект   
BluetoothSerial ESP_BT;
const long interval = 200; 
unsigned long TimeCombination;

void StartSetting(){
    BLEDevice::deinit(); 
    ESP_BT.begin("Keyboard_Setting", "1212");
    Serial.println("Bluetooth с паролем 1212");
    digitalWrite(LED_PIN, 0);
    const uint8_t* mac = esp_bt_dev_get_address();
    if (mac != NULL) {
        Serial.print("MAC: ");
        for (int i = 0; i < 6; i++) {
            Serial.printf("%02X", mac[i]);
            if (i < 5) Serial.print(":");
        }
        Serial.println();
    } else {
        Serial.println("MAC not available");
    }

    while (true){
        if (ESP_BT.available()){
            String massage = ESP_BT.readString();
            Serial.print("Получено: "); Serial.println(massage);
        }
        if (digitalRead(ButtonPins[0]) == LOW) {
            ESP_BT.end();
            ESP.restart(); 
        }
    }  
}

void setup() {
    for (int PinBut; PinBut < ButtonCount; PinBut++) {
        pinMode(ButtonPins[PinBut], INPUT_PULLUP);
        TimeAndFlag[PinBut].key = PinBut;
        TimeAndFlag[PinBut].TimeMillisBut = 0;
        TimeAndFlag[PinBut].LetGoFlagBut = false; 
    }
    pinMode(LED_PIN, OUTPUT); 
    Serial.begin(115200);
    bleKeyboard.begin();
    Serial.println("BLE Keyboard запущена!");
    
}

void loop() {
    // Если клавиатура подключена то встроенный светодиод горит
    bool connected = bleKeyboard.isConnected();
    digitalWrite(LED_PIN, connected);
    unsigned long Time = millis();

    if (digitalRead(ButtonPins[2]) == LOW && digitalRead(ButtonPins[3]) == LOW){
        if (Time - TimeCombination >= 3000){
            StartSetting();
        }
    }else{TimeCombination = Time;}

    for (int idKey=0; idKey < ButtonCount; idKey++){
        if (digitalRead(ButtonPins[idKey]) == LOW){
            if (Time - TimeAndFlag[idKey].TimeMillisBut >= interval){
                TimeAndFlag[idKey].TimeMillisBut = Time;
                if (TimeAndFlag[idKey].LetGoFlagBut == false){
                    TimeAndFlag[idKey].LetGoFlagBut = true;
                    Serial.print("Клавиша "); Serial.println(idKey + 1);

                    for (int combinationSchet=0; combinationSchet<4; combinationSchet++) {
                        if (MakrosKey[idKey].combination[combinationSchet] != 0){
                            bleKeyboard.press(MakrosKey[idKey].combination[combinationSchet]);
                        }
                    }
                    bleKeyboard.releaseAll();
                }
            }
        }else{TimeAndFlag[idKey].LetGoFlagBut = false;}
    
    }

}