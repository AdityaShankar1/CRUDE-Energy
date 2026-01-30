import { useEffect, useState } from 'react';
import { assetService } from './api/assetService';
// Notice 'type' before EnergyAsset, but NodeStatus is imported normally
import { type EnergyAsset, NodeStatus } from './types/index';
function App() {
    const [assets, setAssets] = useState<EnergyAsset[]>([]);
    const [loading, setLoading] = useState(true);

    const loadData = async () => {
        try {
            const data = await assetService.getAssets();
            setAssets(data);
            setLoading(false);
        } catch (e) {
            console.error("Backend unreachable. Ensure your .NET API is running at http://localhost:5189", e);
        }
    };

    useEffect(() => {
        loadData();
        const timer = setInterval(loadData, 3000); // Polling every 3 seconds
        return () => clearInterval(timer);
    }, []);

    if (loading && assets.length === 0) {
        return (
            <div className="min-h-screen bg-slate-950 flex items-center justify-center">
                <div className="text-emerald-400 animate-pulse font-mono">CONNECTING TO CRUDE_DB...</div>
            </div>
        );
    }

    return (
        <div className="min-h-screen bg-slate-950 text-slate-100 p-8 font-sans">
            <div className="max-w-6xl mx-auto">
                {/* Header Section */}
                <div className="flex justify-between items-end mb-12 border-b border-slate-800 pb-8">
                    <div>
                        <h1 className="text-5xl font-black tracking-tighter text-emerald-400">
                            CRUDE<span className="text-slate-500">.</span>MONITOR
                        </h1>
                        <p className="text-slate-500 uppercase tracking-widest text-xs mt-2 font-bold">
                            Energy Consumption Evaluation Engine
                        </p>
                    </div>
                    <div className="text-right">
                        <div className="flex items-center gap-2 justify-end">
              <span className="relative flex h-3 w-3">
                <span className="animate-ping absolute inline-flex h-full w-full rounded-full bg-emerald-400 opacity-75"></span>
                <span className="relative inline-flex rounded-full h-3 w-3 bg-emerald-500"></span>
              </span>
                            <span className="text-emerald-500 font-bold text-sm tracking-tight">LIVE SYSTEM FEED</span>
                        </div>
                        <p className="text-[10px] text-slate-600 mt-1 uppercase">Updates every 3s</p>
                    </div>
                </div>

                {/* Dashboard Grid */}
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
                    {assets.map((asset) => (
                        <div
                            key={asset.id}
                            className={`relative overflow-hidden p-8 rounded-3xl border transition-all duration-500 shadow-2xl ${
                                asset.status === NodeStatus.Flagged
                                    ? 'border-red-500 bg-red-950/20 shadow-red-900/10'
                                    : 'border-slate-800 bg-slate-900/50 hover:border-slate-700'
                            }`}
                        >
                            <div className="flex justify-between items-start mb-8">
                                <div>
                                    <h2 className="text-2xl font-bold tracking-tight text-white">{asset.name}</h2>
                                    <p className="text-[10px] text-slate-500 font-mono mt-1">{asset.id.substring(0, 8)}</p>
                                </div>
                                <div className={`px-3 py-1 rounded-md text-[10px] font-black uppercase tracking-tighter ${
                                    asset.status === NodeStatus.Flagged ? 'bg-red-500 text-white' : 'bg-emerald-500 text-slate-950'
                                }`}>
                                    {NodeStatus[asset.status]}
                                </div>
                            </div>

                            <div className="space-y-1">
                                <p className="text-slate-500 text-[10px] font-bold uppercase tracking-widest">Real-time Load</p>
                                <div className="text-6xl font-mono tracking-tighter tabular-nums text-white">
                                    {asset.currentReading.toFixed(1)}
                                    <span className="text-xl text-slate-700 ml-2">kW</span>
                                </div>
                            </div>

                            <div className="mt-10 pt-6 border-t border-slate-800/50 flex justify-between items-center">
                                <div>
                                    <p className="text-slate-600 text-[10px] uppercase font-bold tracking-widest">Threshold</p>
                                    <p className="text-orange-400 font-mono font-bold">{asset.maxConsumptionThreshold} kW</p>
                                </div>
                                <div className="text-right">
                                    <p className="text-slate-600 text-[10px] uppercase font-bold tracking-widest">Last Update</p>
                                    <p className="text-slate-400 text-[10px] font-mono">
                                        {new Date(asset.lastUpdated).toLocaleTimeString()}
                                    </p>
                                </div>
                            </div>

                            {/* Critical Alert Overlay */}
                            {asset.status === NodeStatus.Flagged && (
                                <div className="absolute top-0 right-0 left-0 h-1 bg-red-500 animate-pulse"></div>
                            )}
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
}

export default App;