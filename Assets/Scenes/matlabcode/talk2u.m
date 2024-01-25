clear all
global rM aM

% screen manager
s = screenManager;
s.screen = 2;
s.disableSyncTests = true;
s.distance = 60;

s2 = screenManager;
s2.screen = 1;
s2.windowed = [2000 0 3000 700];

% eye tracker
t = tobiiManager;
t.useOperatorScreen = true;
t.isDummy = false;
t.calibration.mode = 'human';

d = dataConnection;
d.protocol = 'tcp';
% I am a client
d.rPort = 59250;
d.open;

rM = arduinoManager;
aM = audioManager;
rM.silentMode = false;
aM.silentMode = true;
rM.open();
aM.open();

RestrictKeysForKbCheck(KbName('q'));

open(s);

initialise(t, s, s2);

trackerSetup(t); % calibration

WaitSecs(0.5);
close(s);
t.win = [];

t.startRecording(true);

while ~KbCheck

	t.getSample; % get current X and Y position;
	try t.trackerDrawEyePosition; end % draw to operator screen
	t.trackerFlip; % flip operator screen

	msg = d.read; % read network message
	if ~isempty(msg)
        switch msg
            case 'reward'
                rM.timedTTL;
            case 'calibrate'
                open(s);
                t.trackerSetup;
                close(s);
            otherwise
                t.trackerMessage(msg,GetSecs);
                disp(msg);
        end
	end

end

t.stopRecording(true);
t.saveData();
t.close;
d.close;
rM.close;
aM.close;
