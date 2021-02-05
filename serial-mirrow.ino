void setup() {
    Serial.begin(115200);
    //Serial.begin(9600);
    Serial.write('B');
}

void loop() {
    if (Serial.available() > 0) {
        Serial.write(Serial.read());
    }
}
