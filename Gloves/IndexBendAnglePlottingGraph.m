 
a = csvread('report.csv',1,0);

x = a(:,1);
y = a(:,2:4);

[n,m] = size(x);
M = max(y(:))+5; 
T = max(x(:))/1000;
divisor = 20;
numberOfFrames = floor(n/divisor);
for i = 1:numberOfFrames  
    j = divisor*i;
    plot(x(1:j)/1000,y(1:j,:));
    xlim([0 T])
    ylim([0 M])
    xlabel('Time (s)')
    ylabel('Angle (degrees)')
    title('Plot of Index MCP, PIP & DIP Bend Angles against Time')
    legend({'MCP','PIP','DIP'},'Location','northeast')
end
figure;
x2 = a(:,1);
y2 = a(:,10);

[n,m] = size(x2);
M = max(y2(:))+0.5; 
T = max(x2(:))/1000;
for i = 1:numberOfFrames  
    j = divisor*i;
    plot(x2(1:j)/1000,y2(1:j,:));
    xlim([0 T])
    ylim([0 M])
    xlabel('Time (s)')
    ylabel('Stiffness')
    title('Plot of Force Feedback on Index Fingertip against Time')
end
figure;

x3 = a(:,1);
y3 = a(:,5);
[n,m] = size(x3);
M = 10+max(y3(:)); 
T = max(x3(:));
for i = 1:numberOfFrames  
    j = divisor*i;
    plot(x3(1:j),y3(1:j,:));
    xlim([0 T])
    ylim([0 M])
    xlabel('Time (ms)')
    ylabel('Angle (degrees)')
    title('Plot of Index MCP Split Angle against Time')
    legend({'MCP'},'Location','northeast')
end