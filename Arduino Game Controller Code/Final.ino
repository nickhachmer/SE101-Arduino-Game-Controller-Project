/* Arduino USB HID Keyboard Demo
  Random Key/Random Delay
*/
uint8_t buf[8] = {0}; /* Keyboard report buffer */

/*LCD Declarations and funcitons*/
int DI = 12;
int RW = 11;
int DB[] = {3, 4, 5, 6, 7, 8, 9, 10};//Use an array to define the pins 
int Enable = 2;

void LcdCommandWrite(int value) {
 // Define all pins
 int i = 0;
 for (i=DB[0]; i <= DI; i++) //Assignment
{
   digitalWrite(i,value & 01);//Because 1602 LCD signal identification is D7-D0 (not D0-D7), here is used to invert the signal.
   value >>= 1;
 }
 digitalWrite(Enable,LOW);
 delayMicroseconds(1);
 digitalWrite(Enable,HIGH);
 delayMicroseconds(1);  
 digitalWrite(Enable,LOW);
 delayMicroseconds(1);  
}
 
void LcdDataWrite(int value) {
 // Define all pins
 int i = 0;
 digitalWrite(DI, HIGH);
 digitalWrite(RW, LOW);
 for (i=DB[0]; i <= DB[7]; i++) {
   digitalWrite(i,value & 01);
   value >>= 1;
 }
 digitalWrite(Enable,LOW);
 delayMicroseconds(1);
 digitalWrite(Enable,HIGH);
 delayMicroseconds(1);
 digitalWrite(Enable,LOW);
 delayMicroseconds(1);  
 }
void setup (void) {
 int i = 0;
 for (i=Enable; i <= DI; i++) {
   pinMode(i,OUTPUT);
 }
 delay(100);
 // Initialize the LCD
 LcdCommandWrite(0x38);  // Set to 8-bit interface, 2 lines display, 5x7 text size                    
 delay(64);                      
 LcdCommandWrite(0x38);  // Set to 8-bit interface, 2 lines display, 5x7 text size                      
 delay(50);                      
 LcdCommandWrite(0x38);  // Set to 8-bit interface, 2 lines display, 5x7 text size                     
 delay(20);                      
 LcdCommandWrite(0x06);  // Input method setting
                         // Auto increment, no shift is displayed
 delay(20);                      
 LcdCommandWrite(0x0E);  // display setting
                         // Turn on the display, the cursor shows, no flicker
 delay(20);                      
 LcdCommandWrite(0x01);  // The screen is empty and the cursor position is zeroed
 delay(100);                      
 LcdCommandWrite(0x80);  // display setting
                         //Turn on the display, the cursor shows, no flicker

 delay(20);   
 Serial.begin(9600);
 pinMode(9, INPUT_PULLUP);
 randomSeed(analogRead(0));
 i = 0;
 for (i=Enable; i <= DI; i++) {
   pinMode(i,OUTPUT);
 }
 delay(200);
}

void loop() {
  
  if (analogRead(A0) < 510) {
    buf[0] = 0;
    buf[2] = 0x4F; // right arrow 0x4F
    Serial.write(buf, 8);
    LcdDataWrite('R');
    LcdDataWrite('i');
    LcdDataWrite('g');
    LcdDataWrite('h');
    LcdDataWrite('t');
    delay(150);
  } else if (analogRead(A0) > 530) {
    buf[0] = 0;
    buf[2] = 0x50; // left arrow 0x50
    Serial.write(buf, 8);
     LcdDataWrite('l'); 
     LcdDataWrite('e');
     LcdDataWrite('f');
     LcdDataWrite('t');
     delay(150);
  }
  releaseKey();
  if (digitalRead(9) == LOW) {
    buf[0] = 0;
    buf[2] = 0x52; // up arrow 0x52
    Serial.write(buf, 8);
    LcdDataWrite('J');
    LcdDataWrite('u');
    LcdDataWrite('m');
    LcdDataWrite('p');
    delay(5);
  }
  releaseKey();
    
  if (digitalRead(8) == LOW) {
    buf[0] = 0;
    buf[2] = 0x2c; // space key
    Serial.write(buf, 8);
    LcdDataWrite('S');
    LcdDataWrite('h');
    LcdDataWrite('o');
    LcdDataWrite('o');
    LcdDataWrite('t');
    delay(10);
   }
   releaseKey();
   delay(20); 
}

void releaseKey() {
  buf[0] = 0;
  buf[2] = 0;
  Serial.write(buf, 8); // Release key
}

 
